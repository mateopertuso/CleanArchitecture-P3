using CasosUso.InterfacesCU;
using Excepciones;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class CUDevolverPrestamo : IDevolverPrestamo
    {
        private readonly IRepositorioPrestamos _repoPrestamos;
        private readonly IRepositorioAuditoriaPrestamos _repoAuditorias;
        private readonly IRepositorioUsuarios _repoUsuarios;

        public CUDevolverPrestamo(IRepositorioPrestamos repoPrestamos, IRepositorioAuditoriaPrestamos repoAuditorias,
            IRepositorioUsuarios repoUsuarios)
        {
            _repoPrestamos = repoPrestamos;
            _repoAuditorias = repoAuditorias;
            _repoUsuarios = repoUsuarios;
        }

        public void Ejecutar(int prestamoId, int coordinadorId)
        {
            var prestamo = _repoPrestamos.FindById(prestamoId);

            if (prestamo == null)
                throw new DatosInvalidosException("No existe el préstamo");

            Usuario? coordinador = _repoUsuarios.FindById(coordinadorId);

            if (coordinador == null)
            {
                throw new DatosInvalidosException("No existe el coordinador");
            }

            prestamo.Devolver();

            _repoPrestamos.Update(prestamo);

            AuditoriaPrestamo auditoria = new AuditoriaPrestamo
            {
                Fecha = DateTime.Now,
                Accion = TipoAccionAuditoria.DEVOLUCION,
                PrestamoId = prestamo.Id,
                UsuarioId = coordinador.Id
            };

            _repoAuditorias.Add(auditoria);
        }
    }
}
