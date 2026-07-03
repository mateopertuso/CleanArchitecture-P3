using CasosUso.DTOs;
using LogicaNegocio.ClasesDominio;

namespace CasosUso.InterfacesCU
{
    public interface IEvaluadorObservacionIA
    {
        EvaluacionObservacionDTO Evaluar(Prestamo prestamo, ObjetoCeleste objeto);
    }
}