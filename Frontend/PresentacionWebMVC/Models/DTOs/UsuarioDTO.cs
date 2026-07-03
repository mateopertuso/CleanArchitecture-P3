namespace PresentacionWebMVC.Models.DTOs
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Rol { get; set; }
        public string? Token { get; set; }
    }
}
