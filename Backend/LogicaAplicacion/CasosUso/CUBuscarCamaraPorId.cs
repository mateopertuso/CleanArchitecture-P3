using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarCamaraPorId : IBuscarCamaraPorId
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUBuscarCamaraPorId(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public CamaraDTO BuscarPorId(int id)
        {
            Equipo? equipo = RepoEquipos.FindById(id);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe la cámara");
            }

            Camara camara = (Camara)equipo;

            return MapperEquipos.ToCamaraDTO(camara);
        }
    }
}