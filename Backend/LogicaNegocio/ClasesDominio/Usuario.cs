using Excepciones;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.ClasesDominio
{
    public class Usuario : IValidable
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public required Email Email { get; set; }
        public required string Username { get; set; }
        public required Password Password { get; set; }
        public required Rol Rol { get; set; }

        public void Validar()
        {
            if (NombreCompleto == null) { throw new DatosInvalidosException("El nombre del usuario es obligatorio"); }
        }
    }
}
