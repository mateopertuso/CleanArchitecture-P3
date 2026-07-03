using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarMonturaPorId
    {
        MonturaDTO BuscarPorId(int id);
    }
}