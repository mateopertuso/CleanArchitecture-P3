using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IDevolverPrestamo
    {
        void Ejecutar(int prestamoId, int coordinadorId); //coordinador para auditoria
    }
}
