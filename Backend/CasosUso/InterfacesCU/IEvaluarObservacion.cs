using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IEvaluarObservacion
    {
        EvaluacionObservacionDTO Evaluar(int prestamoId, int objetoCelesteId);
    }
}