using CasosUso.DTOs;

public interface IListadoPrestamosActivosUsuario
{
    IEnumerable<PrestamoDTO> ObtenerPorUsuario(int usuarioId);
}