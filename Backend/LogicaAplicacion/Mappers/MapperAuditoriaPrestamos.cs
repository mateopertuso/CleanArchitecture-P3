using CasosUso.DTOs;

public class MapperAuditoriaPrestamos
{
    public static AuditoriaPrestamoDTO ToDTO(AuditoriaPrestamo auditoria)
    {
        return new AuditoriaPrestamoDTO
        {
            Id = auditoria.Id,
            Fecha = auditoria.Fecha,
            Usuario = auditoria.Usuario.NombreCompleto,
            Accion = auditoria.Accion.ToString(),
            PrestamoId = auditoria.PrestamoId
        };
    }
}