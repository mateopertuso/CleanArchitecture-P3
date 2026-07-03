namespace CasosUso.DTOs
{
    public class TelescopioDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public double AperturaMm { get; set; }
        public double RelacionFocal { get; set; }
        public double DistanciaFocalMm { get; set; }
        public double PesoKg { get; set; }
    }
}