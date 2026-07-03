using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using Microsoft.EntityFrameworkCore;

public class
    RepositorioAuditoriaPrestamosBD : IRepositorioAuditoriaPrestamos
{
    public StellarMindsContext Contexto { get; set; }

    public RepositorioAuditoriaPrestamosBD(StellarMindsContext contexto)
    {
        Contexto = contexto;
    }

    public void Add(AuditoriaPrestamo nueva)
    {
        Contexto.AuditoriasPrestamos
            .Add(nueva);

        Contexto.SaveChanges();
    }

    public IEnumerable<AuditoriaPrestamo> FindAll()
    {
        return Contexto
            .AuditoriasPrestamos
            .Include(a => a.Usuario)
            .Include(a => a.Prestamo)
            .ToList();
    }

    public AuditoriaPrestamo? FindById(int id)
    {
        return Contexto
            .AuditoriasPrestamos
            .Include(a => a.Usuario)
            .Include(a => a.Prestamo)
            .FirstOrDefault(a => a.Id == id);
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(AuditoriaPrestamo obj)
    {
        throw new NotImplementedException();
    }


    public IEnumerable<Prestamo> ObtenerPrestamosPorCoordinador(int coordinadorId)
    {
        return Contexto.AuditoriasPrestamos
            .Include(a => a.Prestamo)
                .ThenInclude(p => p.Usuario)
            .Include(a => a.Prestamo)
                .ThenInclude(p => p.Telescopio)
            .Include(a => a.Prestamo)
                .ThenInclude(p => p.Montura)
            .Include(a => a.Prestamo)
                .ThenInclude(p => p.Camara)
            .Include(a => a.Prestamo)
                .ThenInclude(p => p.Ocular)
            .Where(a => a.UsuarioId == coordinadorId)
            .Select(a => a.Prestamo)
            .GroupBy(p => p.Id) //para no ver ambas acciones por separado, mismo prestamo se ve 1 vez
            .Select(g => g.First())
            .ToList();
    }

    public IEnumerable<AuditoriaPrestamo>ObtenerPorPrestamo(int prestamoId)
    {
        return Contexto.AuditoriasPrestamos
            .Include(a => a.Usuario)
            .Include(a => a.Prestamo)
            .Where(a => a.PrestamoId == prestamoId)
            .OrderBy(a => a.Fecha)
            .ToList();
    }


}