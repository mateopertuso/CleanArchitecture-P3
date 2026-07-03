using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarMonturaPorId : IBuscarMonturaPorId
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUBuscarMonturaPorId(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public MonturaDTO BuscarPorId(int id)
        {
            Equipo? equipo = RepoEquipos.FindById(id);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe la montura");
            }

            Montura montura = (Montura)equipo;

            return MapperEquipos.ToMonturaDTO(montura);
        }
    }
}