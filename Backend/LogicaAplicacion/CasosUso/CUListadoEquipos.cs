using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUListadoEquipos : IListadoEquipos
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUListadoEquipos(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public IEnumerable<EquipoDTO> ObtenerListado()
        {
            return RepoEquipos
                .FindAll()
                .Select(MapperEquipos.ToEquipoDTO)
                .ToList();
        }
    }
}