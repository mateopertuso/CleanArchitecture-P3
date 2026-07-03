using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioObservacionesBD : IRepositorioObservaciones
    {
        public StellarMindsContext Contexto { get; set; }

        public
            RepositorioObservacionesBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Observacion nueva)
        {
            nueva.Validar();

            Contexto
                .Observaciones
                .Add(nueva);

            Contexto.SaveChanges();
        }

        public IEnumerable<Observacion> FindAll()
        {
            return Contexto
                .Observaciones
                .Include(o => o.Prestamo)
                .Include(o => o.ObjetoCeleste)
                .ToList();
        }

        public Observacion? FindById(int id)
        {
            return Contexto
                .Observaciones
                .Include(o => o.Prestamo)
                .Include(o => o.ObjetoCeleste)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Observacion obj)
        {
            throw new NotImplementedException();
        }

        public bool ExisteObservacion(DateTime fecha, int prestamoId, int objetoCelesteId)
        {
            return Contexto.Observaciones.Any(o =>
                o.Fecha.Date == fecha.Date &&
                o.Prestamo.Id == prestamoId &&
                o.ObjetoCeleste.Id == objetoCelesteId);
        }
    }
}