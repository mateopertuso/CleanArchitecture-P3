using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

public interface IRepositorioAuditoriaPrestamos : IRepositorio<AuditoriaPrestamo>
{
    IEnumerable<Prestamo> ObtenerPrestamosPorCoordinador(int coordinadorId);
    IEnumerable<AuditoriaPrestamo> ObtenerPorPrestamo(int prestamoId);
}