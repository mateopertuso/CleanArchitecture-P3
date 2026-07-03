using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarTelescopioPorId
    {
        TelescopioDTO BuscarPorId(int id);
    }
}