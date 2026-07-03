using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarTelescopioPorId : IBuscarTelescopioPorId
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUBuscarTelescopioPorId(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public TelescopioDTO BuscarPorId(int id)
        {
            Equipo? equipo = RepoEquipos.FindById(id);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe el telescopio");
            }

            Telescopio telescopio = (Telescopio)equipo;

            return MapperEquipos.ToTelescopioDTO(telescopio);
        }
    }
}