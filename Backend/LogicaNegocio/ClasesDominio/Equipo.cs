using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public abstract class Equipo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }

        public bool TieneDisponibilidad()
        {
            return CantidadDisponible > 0;
        }

        public void ReducirStock()
        {
            if (CantidadDisponible <= 0)
                throw new Exception("No hay stock disponible");

            CantidadDisponible--;
        }

        public void AumentarStock()
        {
            CantidadDisponible++;
        }
    }
}
