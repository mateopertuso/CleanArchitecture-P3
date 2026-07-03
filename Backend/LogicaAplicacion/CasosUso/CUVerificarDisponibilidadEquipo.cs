using CasosUso.InterfacesCU;
using Excepciones;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUVerificarDisponibilidadEquipo : IVerificarDisponibilidadEquipo
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUVerificarDisponibilidadEquipo(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public bool EstaDisponible(int equipoId)
        {
            var equipo = RepoEquipos.FindById(equipoId);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe el equipo");
            }

            return equipo.TieneDisponibilidad();
        }
    }
}