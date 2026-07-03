using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarCamaraPorId
    {
        CamaraDTO BuscarPorId(int id);
    }
}