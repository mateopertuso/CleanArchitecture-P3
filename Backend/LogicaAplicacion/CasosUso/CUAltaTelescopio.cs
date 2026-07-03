using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUAltaTelescopio : IAltaTelescopio
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUAltaTelescopio(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarAlta(AltaTelescopioDTO dto)
        {
            Telescopio telescopio = MapperEquipos.ToTelescopio(dto);

            RepoEquipos.Add(telescopio);
        }
    }
}