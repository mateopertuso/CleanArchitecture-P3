namespace CasosUso.DTOs
{
    public class PrestamoDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }

        public string Usuario { get; set; }
        public string Telescopio { get; set; }
        public string Montura { get; set; }
        public string? Camara { get; set; }
        public string? Ocular { get; set; }
    }
}
