using CasosUso.DTOs;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;

public class CUListadoPrestamosSocioEntreFechas : IListadoPrestamosSocioEntreFechas
{
    public IRepositorioPrestamos RepoPrestamos {get; set;}

    public CUListadoPrestamosSocioEntreFechas(IRepositorioPrestamos repo)
    {
        RepoPrestamos = repo;
    }

    public List<PrestamoListadoDTO> Obtener(int usuarioId, int mes, int anio)
    {
        var prestamos = RepoPrestamos.ObtenerPrestamosSocio(usuarioId, mes, anio);

        return prestamos.Select(p => new PrestamoListadoDTO
            {
                Id = p.Id,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Estado = p.Estado.ToString(),
                Telescopio = $"{p.Telescopio.Marca} {p.Telescopio.Modelo}",
                Atrasado = p.Estado == EstadoPrestamo.EN_PRESTAMO && DateTime.Now > p.FechaFin
            }
        ).ToList();
    }
}