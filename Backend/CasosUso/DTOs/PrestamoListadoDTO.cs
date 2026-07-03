namespace CasosUso.DTOs
{
    public class PrestamoListadoDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
        public bool Atrasado { get; set; }
        public string Telescopio { get; set; }
    }
}