using CasosUso.DTOs;
using LogicaNegocio.ClasesDominio;

namespace LogicaAplicacion.Mappers
{
    public class MapperObjetosCelestes
    {
        public static ObjetoCelesteDTO ToDTO(ObjetoCeleste obj)
        {
            return new ObjetoCelesteDTO
            {
                Id = obj.Id,
                Nombre = obj.Nombre,
                Tipo = obj.Tipo.ToString(),
                MagnitudAparente = obj.MagnitudAparente
            };
        }
    }
}