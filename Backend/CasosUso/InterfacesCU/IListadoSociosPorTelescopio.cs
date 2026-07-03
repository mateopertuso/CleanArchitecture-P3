using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IListadoSociosPorTelescopio
    {
        IEnumerable<SocioPrestamoDTO> Obtener(int telescopioId);
    }
}