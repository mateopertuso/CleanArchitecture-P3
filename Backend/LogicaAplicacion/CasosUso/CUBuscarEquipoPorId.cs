using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarEquipoPorId : IBuscarEquipoPorId
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUBuscarEquipoPorId(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public EquipoDTO BuscarPorId(int id)
        {
            var equipo = RepoEquipos.FindById(id);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe el equipo");
            }

            return MapperEquipos.ToEquipoDTO(equipo);
        }
    }
}