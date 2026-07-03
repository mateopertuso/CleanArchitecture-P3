using LogicaNegocio.Enums;

namespace PresentacionWebMVC.Models.DTOs
{
    public class MonturaDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public TipoMontura TipoMontura { get; set; }
        public double CargaMaximaKg { get; set; }
        public bool EsGoTo { get; set; }
    }
}
