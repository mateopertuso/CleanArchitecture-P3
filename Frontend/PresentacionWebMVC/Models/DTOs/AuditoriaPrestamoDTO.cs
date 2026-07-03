namespace PresentacionWebMVC.Models.DTOs
{
    public class AuditoriaPrestamoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Accion {  get; set; }
        public int PrestamoId { get; set; }
    }
}
