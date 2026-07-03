using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarEquipoPorId
    {
        EquipoDTO BuscarPorId(int id);
    }
}