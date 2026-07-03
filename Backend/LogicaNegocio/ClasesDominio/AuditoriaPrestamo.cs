using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;

public class AuditoriaPrestamo
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public TipoAccionAuditoria Accion { get; set; }
    public int PrestamoId { get; set; }
    public Prestamo Prestamo { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}