using Excepciones;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public class Prestamo : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoPrestamo Estado { get; set; }

        public int UsuarioId { get; set; }
        public required Usuario Usuario { get; set; }
        public int TelescopioId { get; set; }
        public required Telescopio Telescopio { get; set; }
        public int MonturaId { get; set; }
        public required Montura Montura { get; set; }
        public int? CamaraId { get; set; }
        public Camara? Camara { get; set; }
        public int? OcularId { get; set; }
        public Ocular? Ocular { get; set; }

        public bool EstaVigente()
        {
            return Estado == EstadoPrestamo.EN_PRESTAMO &&
                   DateTime.Now >= FechaInicio &&
                   DateTime.Now <= FechaFin;
        }

        public void Inicializar()
        {
            FechaInicio = DateTime.Now;
            Estado = EstadoPrestamo.EN_PRESTAMO;
        }


        public void Validar()
        {
            if (Usuario == null)
                throw new DatosInvalidosException("Debe existir un usuario");

            if (Telescopio == null || Montura == null)
                throw new DatosInvalidosException("Debe seleccionar telescopio y montura");

            if (Camara == null && Ocular == null)
                throw new DatosInvalidosException("Debe seleccionar al menos cámara u ocular");

            if (FechaFin <= DateTime.Now)
                throw new DatosInvalidosException("La fecha fin debe ser posterior a la fecha actual");

            if (!Montura.SoportaPeso(Telescopio.PesoKg))
                throw new DatosInvalidosException("La montura no soporta el peso del telescopio");

            if (Camara != null && Montura.TipoMontura == TipoMontura.ALTAZIMUTAL)
                throw new DatosInvalidosException("No se puede usar cámara con montura altazimutal");

            if (!Telescopio.TieneDisponibilidad())
                throw new DatosInvalidosException("No hay telescopios disponibles");

            if (!Montura.TieneDisponibilidad())
                throw new DatosInvalidosException("No hay monturas disponibles");

            if (Camara != null && !Camara.TieneDisponibilidad())
                throw new DatosInvalidosException("No hay cámaras disponibles");

            if (Ocular != null && !Ocular.TieneDisponibilidad())
                throw new DatosInvalidosException("No hay oculares disponibles");
        }


        public void ConfirmarPrestamo()
        {
            Telescopio.ReducirStock();
            Montura.ReducirStock();

            if (Camara != null) Camara.ReducirStock();
            if (Ocular != null) Ocular.ReducirStock();
        }

        public void Devolver()
        {
            if(Estado != EstadoPrestamo.EN_PRESTAMO)
                throw new DatosInvalidosException("El préstamo no está activo");

            Estado = EstadoPrestamo.DEVUELTO;

            Telescopio.AumentarStock();
            Montura.AumentarStock();

            if(Camara != null) Camara.AumentarStock();
            if(Ocular != null) Ocular.AumentarStock();
        }


    }
}
