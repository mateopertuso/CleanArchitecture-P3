namespace PresentacionWebMVC.Models.DTOs
{
    public class ModificarTelescopioDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public int AperturaMm { get; set; }
        public double RelacionFocal { get; set; }
        public int DistanciaFocalMm { get; set; }
        public double PesoKg { get; set; }
    }
}
