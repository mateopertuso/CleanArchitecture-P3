using LogicaNegocio.ClasesDominio;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEquipos : IRepositorio<Equipo>
    {
        IEnumerable<Equipo> ObtenerDisponibles();
        IEnumerable<Telescopio> ObtenerTelescopios();
        IEnumerable<Montura> ObtenerMonturas();
        IEnumerable<Camara> ObtenerCamaras();
        IEnumerable<Ocular> ObtenerOculares();
    }
}