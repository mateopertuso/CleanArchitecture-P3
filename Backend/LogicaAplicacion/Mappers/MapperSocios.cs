using CasosUso.DTOs;
using LogicaNegocio.ClasesDominio;

namespace LogicaAplicacion.Mappers
{
    public class MapperSocios
    {
        public static SocioPrestamoDTO ToDTO(Usuario usuario)
        {
            return new SocioPrestamoDTO
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email.Valor
            };
        }
    }
}