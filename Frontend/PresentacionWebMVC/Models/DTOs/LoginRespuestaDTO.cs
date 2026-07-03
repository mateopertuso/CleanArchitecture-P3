namespace PresentacionWebMVC.Models.DTOs
{
    public class LoginRespuestaDTO
    {
        public int? UsuarioId { get; set; }
        public string? Username { get; set; }
        public string? Rol { get; set; }
        public string? Token { get; set; }
    }
}
