using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;

public class CUBuscarAuditoriaPrestamo : IBuscarAuditoriaPrestamo
{
    private readonly IRepositorioAuditoriaPrestamos _repo;

    public CUBuscarAuditoriaPrestamo(IRepositorioAuditoriaPrestamos repo)
    {
        _repo = repo;
    }

    public AuditoriaPrestamoDTO Obtener(int id)
    {
        AuditoriaPrestamo auditoria = _repo.FindById(id);

        if (auditoria == null)
        {
            throw new DatosInvalidosException("No existe la auditoría");
        }

        return MapperAuditoriaPrestamos.ToDTO(auditoria);
    }
}