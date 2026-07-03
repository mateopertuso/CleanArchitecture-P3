using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class AltaCamaraDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public TipoSensor TipoSensor { get; set; }
        public string Resolucion { get; set; }
        public double TamanoPixelUm { get; set; }
    }
}
