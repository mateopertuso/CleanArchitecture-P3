using CasosUso.DTOs;

public interface IListadoAuditoriasPrestamos
{
    IEnumerable<AuditoriaPrestamoDTO> Obtener(int prestamoId);
}