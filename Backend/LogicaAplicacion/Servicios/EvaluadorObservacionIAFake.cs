using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaNegocio.ClasesDominio;

namespace LogicaAplicacion.Servicios
{
    public class EvaluadorObservacionIAFake : IEvaluadorObservacionIA
    {
        public EvaluacionObservacionDTO Evaluar(Prestamo prestamo, ObjetoCeleste objeto)
        {
            return new EvaluacionObservacionDTO
            {
                Indicador = "ADECUADO",
                Detalle = "Configuración válida para observación."
            };
        }
    }
}