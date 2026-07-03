using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class AltaUsuarioDTO
    {
        public required string NombreCompleto { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Rol { get; set; }
    }
}
