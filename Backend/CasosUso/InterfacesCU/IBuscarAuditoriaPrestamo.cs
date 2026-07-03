using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarAuditoriaPrestamo
    {
        AuditoriaPrestamoDTO Obtener(int id);
    }
}
