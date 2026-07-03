using Excepciones;
using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEquiposBD : IRepositorioEquipos
    {
        public StellarMindsContext Contexto { get; set; }

        public RepositorioEquiposBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Equipo nuevo)
        {
            Contexto.Equipos.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return Contexto.Equipos.ToList();
        }

        public Equipo? FindById(int id)
        {
            return Contexto.Equipos.FirstOrDefault(e => e.Id == id);
        }

        public void Remove(int id)
        {
            Equipo? equipo = FindById(id);

            if (equipo == null) throw new OperacionInvalidaException("Equipo no encontrado");

            Contexto.Equipos.Remove(equipo);
            Contexto.SaveChanges();
        }

        public void Update(Equipo obj)
        {
            Equipo? existente = Contexto.Equipos
                                      .AsNoTracking()
                                      .Where(e => e.Id == obj.Id)
                                      .SingleOrDefault();

            if (existente == null) throw new OperacionInvalidaException("No existe el equipo");

            Contexto.Equipos.Update(obj);
            Contexto.SaveChanges();
        }

        public IEnumerable<Equipo> ObtenerDisponibles()
        {
            return Contexto.Equipos
                .Where(e => e.CantidadDisponible > 0)
                .ToList();
        }

        public IEnumerable<Telescopio> ObtenerTelescopios()
        {
            return Contexto.Equipos
                .OfType<Telescopio>()
                .ToList();
        }

        public IEnumerable<Montura> ObtenerMonturas()
        {
            return Contexto.Equipos
                .OfType<Montura>()
                .ToList();
        }

        public IEnumerable<Camara> ObtenerCamaras()
        {
            return Contexto.Equipos
                .OfType<Camara>()
                .ToList();
        }

        public IEnumerable<Ocular> ObtenerOculares()
        {
            return Contexto.Equipos
                .OfType<Ocular>()
                .ToList();
        }
    }
}