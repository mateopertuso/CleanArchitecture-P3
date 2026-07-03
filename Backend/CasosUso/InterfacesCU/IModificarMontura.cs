using CasosUso.DTOs;
using CasosUso.DTOs.Editar_equipos;

namespace CasosUso.InterfacesCU
{
    public interface IModificarMontura
    {
        void EjecutarModificacion(ModificarMonturaDTO dto);
    }
}