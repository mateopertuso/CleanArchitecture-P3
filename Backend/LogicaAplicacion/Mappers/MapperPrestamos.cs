using CasosUso.DTOs;
using LogicaNegocio.ClasesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.Mappers
{
    public class MapperPrestamos
    {
        public static PrestamoDTO ToDTO(Prestamo prestamo)
        {
            return new PrestamoDTO
            {
                Id = prestamo.Id,
                FechaInicio = prestamo.FechaInicio,
                FechaFin = prestamo.FechaFin,
                Estado = prestamo.Estado.ToString(),
                Usuario = prestamo.Usuario.NombreCompleto,
                Telescopio = $"{prestamo.Telescopio.Marca} {prestamo.Telescopio.Modelo}",
                Montura = $"{prestamo.Montura.Marca} {prestamo.Montura.Modelo}",
                Camara = prestamo.Camara != null ? $"{prestamo.Camara.Marca} {prestamo.Camara.Modelo}" : null,
                Ocular = prestamo.Ocular != null ? $"{prestamo.Ocular.Marca} {prestamo.Ocular.Modelo}" : null
            };
        }
    }
}
