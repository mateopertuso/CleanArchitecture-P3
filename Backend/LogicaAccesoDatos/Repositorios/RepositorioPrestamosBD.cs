using Excepciones;
using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioPrestamosBD : IRepositorioPrestamos
    {
        public StellarMindsContext Contexto { get; set; }

        public RepositorioPrestamosBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Prestamo nuevo)
        {
            //validacion ya ocurrio en cucrearprestamo
            Contexto.Prestamos.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Prestamo> FindAll()
        {
            return Contexto.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .ToList();
        }

        public Prestamo? FindById(int id)
        {
            return Contexto.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Remove(int id)
        {
            Prestamo? prestamo = FindById(id);

            if (prestamo == null) throw new OperacionInvalidaException("No existe el préstamo con id: " + id);

            Contexto.Prestamos.Remove(prestamo);
            Contexto.SaveChanges();
        }

        public void Update(Prestamo obj)
        {
            obj.Validar();

            Prestamo? existente = Contexto.Prestamos
                                      .AsNoTracking()
                                      .Where(p => p.Id == obj.Id)
                                      .SingleOrDefault();

            if (existente == null) throw new OperacionInvalidaException("No existe el préstamo");

            Contexto.Prestamos .Update(obj);
            Contexto.SaveChanges();
        }

        //public IEnumerable<Prestamo> FindActivosByUsuario(int usuarioId)
        //{
        //    return Contexto.Prestamos
        //        .Include(p => p.Usuario)
        //        .Include(p => p.Telescopio)
        //        .Include(p => p.Montura)
        //        .Include(p => p.Camara)
        //        .Include(p => p.Ocular)
        //        .Where(p =>
        //            p.UsuarioId == usuarioId &&
        //            p.Estado == EstadoPrestamo.EN_PRESTAMO
        //        )
        //        .ToList();
        //}

        public IEnumerable<Prestamo> FindActivosByUsuario(int usuarioId)
        {
            return Contexto.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .Where(p =>
                    p.UsuarioId == usuarioId &&
                    p.Estado == EstadoPrestamo.EN_PRESTAMO &&
                    p.FechaFin >= DateTime.Now
                )
                .ToList();
        }

        public bool EquipoEstaPrestado(int equipoId) //busco prestamo con tal equipo
        {
            return Contexto.Prestamos.Any(p =>
                p.Estado == EstadoPrestamo.EN_PRESTAMO &&
                (
                    p.Telescopio.Id == equipoId ||
                    p.Montura.Id == equipoId ||
                    (p.Camara != null && p.Camara.Id == equipoId) ||
                    (p.Ocular != null && p.Ocular.Id == equipoId)
                )
            );
        }

        public List<Prestamo> ObtenerPrestamosSocio(int usuarioId, int mes, int anio)
        {
            return Contexto.Prestamos
                .Include(p => p.Telescopio)
                .Where(p =>
                    p.Usuario.Id == usuarioId &&
                    p.FechaInicio.Month == mes &&
                    p.FechaInicio.Year == anio)
                .ToList();
        }

        public IEnumerable<Usuario> ObtenerSociosPorTelescopio(int telescopioId)
        {
            return Contexto.Prestamos
                .Include(p => p.Usuario)
                .ThenInclude(u => u.Rol)
                .Where(p => p.TelescopioId == telescopioId)
                .AsEnumerable() //hasta aca sql, despues linq y se ejecuta en memoria (basicamente el trabajo del AsEnumerable)
                .Select(p => p.Usuario)
                .Where(u => u.Rol.Nombre == "SOCIO")
                .GroupBy(u => u.Id)
                .Select(g => g.First())
                .OrderByDescending(u => u.NombreCompleto)
                .ToList();
        }
    }
}