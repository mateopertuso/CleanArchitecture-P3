using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs.Editar_equipos
{
    public class ModificarMonturaDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public TipoMontura TipoMontura { get; set; }
        public double CargaMaximaKg { get; set; }
        public bool EsGoTo {  get; set; }
    }
}
