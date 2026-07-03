using System.Net.Http.Headers;

namespace PresentacionWebMVC.Auxiliar
{
    public class AuxliarClienteHttp
    {
        public static HttpResponseMessage EnviarSolicitud(string verbo, string url, object obj = null, string token = null)
        {
            HttpClient cliente = new HttpClient();

            if (token != null)
            {
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            HttpResponseMessage respuesta = null;
            Task<HttpResponseMessage> tarea = null;

            if (string.IsNullOrEmpty(verbo) || string.IsNullOrEmpty(url))
            {
                throw new Exception("No se provee verbo o url");
            }

            if (verbo.ToUpper() == "GET")
            {
                tarea = cliente.GetAsync(url);
            }
            else if (verbo.ToUpper() == "POST")
            {
                if (obj == null) throw new Exception("No hay datos para el body de la solicitud");
                tarea = cliente.PostAsJsonAsync(url, obj);
            }
            else if (verbo.ToUpper() == "PUT")
            {
                tarea = cliente.PutAsJsonAsync(url, obj);
            }
            else if (verbo.ToUpper() == "DELETE")
            {
                tarea = cliente.DeleteAsync(url);
            }
            else
            {
                throw new Exception("Verbo inválido");
            }

            tarea.Wait();
            respuesta = tarea.Result;

            return respuesta;
        }


        public static string ObtenerError(HttpResponseMessage respuesta)
        {
            if (respuesta == null) throw new Exception("No se provee una respuesta http");

            var tarea = respuesta.Content.ReadAsStringAsync();
            tarea.Wait();

            return tarea.Result;
        }
    }
}
