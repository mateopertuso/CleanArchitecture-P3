using CasosUso.DTOs;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

public class CUListadoPrestamosActivosUsuario : IListadoPrestamosActivosUsuario
{
    private readonly IRepositorioPrestamos _repoPrestamos;

    public CUListadoPrestamosActivosUsuario(IRepositorioPrestamos repoPrestamos)
    {
        _repoPrestamos = repoPrestamos;
    }

    public IEnumerable<PrestamoDTO> ObtenerPorUsuario(int usuarioId)
    {
        return _repoPrestamos.FindActivosByUsuario(usuarioId)
            .Select(MapperPrestamos.ToDTO);
    }
}