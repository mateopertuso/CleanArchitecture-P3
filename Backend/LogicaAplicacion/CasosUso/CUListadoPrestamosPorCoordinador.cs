using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;

public class CUListadoPrestamosPorCoordinador : IListadoPrestamosPorCoordinador
{
    private readonly IRepositorioAuditoriaPrestamos _repo;

    public CUListadoPrestamosPorCoordinador(IRepositorioAuditoriaPrestamos repo)
    {
        _repo = repo;
    }

    public IEnumerable<PrestamoDTO> Obtener(int coordinadorId)
    {
        return _repo
            .ObtenerPrestamosPorCoordinador(coordinadorId)
            .Select(MapperPrestamos.ToDTO);
    }
}