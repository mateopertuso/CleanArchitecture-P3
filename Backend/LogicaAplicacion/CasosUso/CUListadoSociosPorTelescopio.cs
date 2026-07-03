using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUListadoSociosPorTelescopio : IListadoSociosPorTelescopio
    {
        private readonly IRepositorioPrestamos _repoPrestamos;

        public CUListadoSociosPorTelescopio(IRepositorioPrestamos repoPrestamos)
        {
            _repoPrestamos = repoPrestamos;
        }

        public IEnumerable<SocioPrestamoDTO> Obtener(int telescopioId)
        {
            return _repoPrestamos
                .ObtenerSociosPorTelescopio(telescopioId)
                .Select(MapperSocios.ToDTO);
        }
    }
}