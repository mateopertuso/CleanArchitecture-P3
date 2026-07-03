using CasosUso.DTOs;

public class CUListadoAuditoriasPrestamos : IListadoAuditoriasPrestamos
{
    private readonly IRepositorioAuditoriaPrestamos _repo;

    public CUListadoAuditoriasPrestamos(IRepositorioAuditoriaPrestamos repo)
    {
        _repo = repo;
    }

    public IEnumerable<AuditoriaPrestamoDTO> Obtener(int prestamoId)
    {
        return _repo
            .ObtenerPorPrestamo(prestamoId)
            .Select(MapperAuditoriaPrestamos.ToDTO);
    }
}