using CasosUso.DTOs;
using Excepciones;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.Mappers
{
    public class MapperUsuarios
    {
        public static UsuarioDTO ToDto(Usuario usu)
        {

            if (usu == null) return null;

            UsuarioDTO dto = new UsuarioDTO()
            {
                Id = usu.Id,
                NombreCompleto = usu.NombreCompleto,
                Telefono = usu.Telefono,
                Direccion = usu.Direccion,
                Email = usu.Email.Valor,
                Username = usu.Username,
                Rol = usu.Rol.Nombre,
            };

            return dto;
        }
        public static Usuario ToUsuario(AltaUsuarioDTO dto, Rol rol)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de usuario");

            return new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Email = new Email(dto.Email),
                Username = dto.Username,
                Password = new Password(dto.Password),
                Rol = rol
            };
        }

    }
}
