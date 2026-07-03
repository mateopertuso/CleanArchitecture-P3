using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public class Telescopio : Equipo
    {
        public int AperturaMm { get; set; }
        public double RelacionFocal { get; set; }
        public int DistanciaFocalMm { get; set; }
        public double PesoKg { get; set; }
    }
}
