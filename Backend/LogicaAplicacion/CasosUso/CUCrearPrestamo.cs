using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class CUCrearPrestamo : ICrearPrestamo
    {
        private readonly IRepositorioUsuarios _repoUsuarios;
        private readonly IRepositorioEquipos _repoEquipos;
        private readonly IRepositorioPrestamos _repoPrestamos;
        private readonly IRepositorioAuditoriaPrestamos _repoAuditorias;

        public CUCrearPrestamo(IRepositorioUsuarios repoUsuarios, IRepositorioEquipos repoEquipos, 
            IRepositorioPrestamos repoPrestamos, IRepositorioAuditoriaPrestamos repoAuditorias)
        {
            _repoUsuarios = repoUsuarios;
            _repoEquipos = repoEquipos;
            _repoPrestamos = repoPrestamos;
            _repoAuditorias = repoAuditorias;
        }

        public PrestamoDTO Ejecutar(AltaPrestamoDTO dto)
        {
            Usuario? usuario = _repoUsuarios.FindById(dto.UsuarioId);
            Usuario? coordinador = _repoUsuarios.FindById(dto.CoordinadorId);

            if (usuario == null)
                throw new DatosInvalidosException("No existe el usuario indicado");

            if (coordinador == null)
                throw new DatosInvalidosException(
                    "No existe el coordinador"
                );

            Equipo? equipoTelescopio = _repoEquipos.FindById(dto.TelescopioId);
            Equipo? equipoMontura = _repoEquipos.FindById(dto.MonturaId);

            if (equipoTelescopio == null)
                throw new DatosInvalidosException("No existe el telescopio indicado");

            if (equipoMontura == null)
                throw new DatosInvalidosException("No existe la montura indicada");

            if (equipoTelescopio is not Telescopio telescopio)
                throw new DatosInvalidosException("El equipo indicado no es un telescopio");

            if (equipoMontura is not Montura montura)
                throw new DatosInvalidosException("El equipo indicado no es una montura");

            Camara? camara = null;
            Ocular? ocular = null;

            if (dto.CamaraId.HasValue)
            {
                Equipo? equipoCamara = _repoEquipos.FindById(dto.CamaraId.Value);

                if (equipoCamara == null)
                    throw new DatosInvalidosException("No existe la cámara indicada");

                if (equipoCamara is not Camara camaraEncontrada)
                    throw new DatosInvalidosException("El equipo indicado no es una cámara");

                camara = camaraEncontrada;
            }

            if (dto.OcularId.HasValue)
            {
                Equipo? equipoOcular = _repoEquipos.FindById(dto.OcularId.Value);

                if (equipoOcular == null)
                    throw new DatosInvalidosException("No existe el ocular indicado");

                if (equipoOcular is not Ocular ocularEncontrado)
                    throw new DatosInvalidosException("El equipo indicado no es un ocular");

                ocular = ocularEncontrado;
            }

            Prestamo prestamo = new Prestamo
            {
                Usuario = usuario,
                Telescopio = telescopio,
                Montura = montura,
                Camara = camara,
                Ocular = ocular,
                FechaFin = dto.FechaFin
            };

            prestamo.Inicializar();
            prestamo.Validar();
            prestamo.ConfirmarPrestamo();

            _repoPrestamos.Add(prestamo);

            AuditoriaPrestamo auditoria = new AuditoriaPrestamo
            {
                Fecha = DateTime.Now,
                Accion = TipoAccionAuditoria.CREACION,
                PrestamoId = prestamo.Id,
                UsuarioId = coordinador.Id
            };

            _repoAuditorias.Add(auditoria);

            return MapperPrestamos.ToDTO(prestamo);
        }
    }
}
