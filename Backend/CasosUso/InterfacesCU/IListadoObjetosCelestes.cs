using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IListadoObjetosCelestes
    {
        IEnumerable<ObjetoCelesteDTO> ObtenerTodos();
    }
}