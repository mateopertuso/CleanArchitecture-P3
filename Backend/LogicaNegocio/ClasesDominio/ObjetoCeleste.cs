using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ClasesDominio
{
    public class ObjetoCeleste
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoObjetoCeleste Tipo { get; set; }
        public decimal MagnitudAparente { get; set; }
    }
}
