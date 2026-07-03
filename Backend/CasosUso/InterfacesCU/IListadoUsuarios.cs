using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IListadoUsuarios
    {
        IEnumerable<UsuarioDTO> ObtenerListado();
    }
}