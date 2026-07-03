using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

public class CUBuscarPrestamoPorId : IBuscarPrestamoPorId
{
    private readonly IRepositorioPrestamos _repo;

    public CUBuscarPrestamoPorId(IRepositorioPrestamos repo)
    {
        _repo = repo;
    }

    public PrestamoDTO Obtener(int id)
    {
        Prestamo prestamo = _repo.FindById(id);

        if (prestamo == null)
        {
            throw new DatosInvalidosException("No existe el préstamo");
        }

        return MapperPrestamos.ToDTO(prestamo);
    }
}