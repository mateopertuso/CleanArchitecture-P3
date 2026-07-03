using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public class Camara : Equipo
    {
        public TipoSensor TipoSensor { get; set; }
        public string Resolucion { get; set; }
        public double TamanoPixelUm { get; set; }
    }
}
