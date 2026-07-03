using System.Text;
using System.Text.Json;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaNegocio.ClasesDominio;
using Microsoft.Extensions.Configuration;

namespace LogicaAplicacion.Servicios
{
    public class EvaluadorObservacionIAGemini : IEvaluadorObservacionIA
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        private const string BASE_URL = "https://generativelanguage.googleapis.com/v1beta/models";
        private const string MODELO = "gemini-2.5-flash";
        private const int MAX_REINTENTOS = 3;
        private const int ESPERA_INICIAL_MS = 2000;

        public EvaluadorObservacionIAGemini(IConfiguration config)
        {
            _apiKey = config["GeminiApiKey"]
                ?? throw new ArgumentNullException("GeminiApiKey no está configurada");

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public EvaluacionObservacionDTO Evaluar(Prestamo prestamo, ObjetoCeleste objeto)
        {
            return EvaluarAsync(prestamo, objeto)
                .GetAwaiter()
                .GetResult();
        }

        public async Task<EvaluacionObservacionDTO> EvaluarAsync(Prestamo prestamo, ObjetoCeleste objeto)
        {
            try
            {
                object payload = ConstruirPayload(prestamo, objeto);

                string prompt = ConstruirPrompt(payload);

                string respuestaJson = await LlamarGeminiAPIConReintentos(prompt);

                return ParsearRespuesta(respuestaJson);
            }
            catch (HttpRequestException ex)
            {
                return new EvaluacionObservacionDTO
                {
                    Indicador = "ERROR",
                    Detalle = $"Error de conexión: {ex.Message}"
                };
            }
            catch
            {
                return new EvaluacionObservacionDTO
                {
                    Indicador = "ERROR",
                    Detalle = "No fue posible interpretar la respuesta de Gemini"
                };
            }
        }

        private object ConstruirPayload(Prestamo prestamo, ObjetoCeleste objeto)
        {
            //astrofotografia
            if (prestamo.Camara != null)
            {
                return new
                {
                    telescopio = new
                    {
                        apertura_mm = prestamo.Telescopio.AperturaMm,
                        focal_mm = prestamo.Telescopio.DistanciaFocalMm,
                        relacion_focal = prestamo.Telescopio.RelacionFocal
                    },

                    camara = new
                    {
                        sensor = prestamo.Camara.TipoSensor.ToString(),
                        resolucion_px = prestamo.Camara.Resolucion,
                        pixel_size_um = prestamo.Camara.TamanoPixelUm
                    },

                    objeto_celeste = new
                    {
                        nombre = objeto.Nombre,
                        tipo = objeto.Tipo.ToString()
                    }
                };
            }
            // observación visual directa
            return new
            {
                telescopio = new
                {
                    apertura_mm = prestamo.Telescopio.AperturaMm,
                    focal_mm = prestamo.Telescopio.DistanciaFocalMm,
                    relacion_focal = prestamo.Telescopio.RelacionFocal
                },

                ocular = new
                {
                    ocular_focal_mm = prestamo.Ocular.DiametroMm,
                    campo_aparente_grados = prestamo.Ocular.CampoVisualGrados
                },

                objeto_celeste = new
                {
                    nombre = objeto.Nombre,
                    tipo = objeto.Tipo.ToString()
                }
            };
        }

        private string ConstruirPrompt(object payload)
        {
            string datosJson = JsonSerializer.Serialize(payload);

            return @$"
            Experto en astronomía.
            Evalúa si el equipo es adecuado para observar el objeto celeste.

            Responde SOLO con JSON válido:

            {{
                ""indicador"": ""IDEAL|ADECUADO|NO_RECOMENDABLE"",
                ""detalle"": ""máximo 300 caracteres""
            }}

            Criterios:
                - IDEAL: excelente combinación
                - ADECUADO: funciona con limitaciones
                - NO_RECOMENDABLE: combinación poco adecuada

            Datos:{datosJson}";
        }

        private async Task<string> LlamarGeminiAPIConReintentos(
            string prompt
        )
        {
            int intento = 0;
            int esperaMs = ESPERA_INICIAL_MS;

            while (intento < MAX_REINTENTOS)
            {
                try
                {
                    intento++;

                    return await LlamarGeminiAPI(prompt);
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("503") || ex.Message.Contains("429"))
                {
                    if (intento < MAX_REINTENTOS)
                    {
                        await Task.Delay(esperaMs);

                        esperaMs *= 2;
                    }
                }
            }

            throw new HttpRequestException($"Error al conectar con Gemini después de {MAX_REINTENTOS} intentos");
        }

        private async Task<string> LlamarGeminiAPI(string prompt)
        {
            string url =
                $"{BASE_URL}/{MODELO}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                },

                generationConfig = new
                {
                    temperature = 0.1,
                    maxOutputTokens = 3000
                }
            };

            string json = JsonSerializer.Serialize(requestBody);

            StringContent content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error {response.StatusCode}: {responseJson}"
                );
            }

            return responseJson;
        }

        private EvaluacionObservacionDTO ParsearRespuesta(string responseJson)
        {
            using JsonDocument doc = JsonDocument.Parse(responseJson);

            string texto = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString()
                ?? "";

            texto = texto
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            EvaluacionObservacionDTO? resultado = JsonSerializer.Deserialize<EvaluacionObservacionDTO>(
                    texto,
                    options
                );

            if (resultado == null)
            {
                throw new Exception("No fue posible interpretar la respuesta");
            }

            string[] indicadoresValidos =
            {
                "IDEAL",
                "ADECUADO",
                "NO_RECOMENDABLE"
            };

            if (!indicadoresValidos.Contains(resultado.Indicador))
            {
                throw new Exception("Indicador inválido");
            }

            if (resultado.Indicador == "IDEAL")
            {
                resultado.Detalle = "";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(resultado.Detalle))
                {
                    throw new Exception("El detalle es obligatorio cuando no es IDEAL");
                }

                if (resultado.Detalle.Length > 300)
                {
                    resultado.Detalle = resultado.Detalle.Substring(0, 297) + "...";
                }
            }

            return resultado;
        }
    }
}