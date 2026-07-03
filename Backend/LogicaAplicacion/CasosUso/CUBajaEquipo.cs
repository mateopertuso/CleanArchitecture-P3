using CasosUso.InterfacesCU;
using Excepciones;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBajaEquipo : IBajaEquipo
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public IRepositorioPrestamos RepoPrestamos { get; set; }

        public CUBajaEquipo(
            IRepositorioEquipos repoEquipos,
            IRepositorioPrestamos repoPrestamos)
        {
            RepoEquipos = repoEquipos;
            RepoPrestamos = repoPrestamos;
        }

        public void EjecutarBaja(int id)
        {
            if (id <= 0)
                throw new OperacionInvalidaException("No existe un equipo con id: " + id);

            if (RepoPrestamos.EquipoEstaPrestado(id))
                throw new OperacionInvalidaException("No se puede eliminar el equipo porque está prestado");

            RepoEquipos.Remove(id);
        }
    }
}