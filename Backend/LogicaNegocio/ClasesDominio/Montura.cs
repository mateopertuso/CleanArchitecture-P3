using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public class Montura : Equipo
    {
        public TipoMontura TipoMontura { get; set; }
        public double CargaMaximaKg { get; set; }
        public bool EsGoTo { get; set; }

        public bool SoportaPeso(double peso)
        {
            return CargaMaximaKg >= peso;
        }
    }
}
