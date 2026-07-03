using LogicaNegocio.ClasesDominio;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioObservaciones : IRepositorio<Observacion>
    {
        bool ExisteObservacion(DateTime fecha, int prestamoId, int objetoCelesteId);
    }
}