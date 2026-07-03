using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUModificarTelescopio : IModificarTelescopio
    {
        public IRepositorioEquipos RepoEquipos
        { get; set; }

        public CUModificarTelescopio(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarModificacion(ModificarTelescopioDTO dto)
        {
            Telescopio telescopio = MapperEquipos.ToTelescopio(dto);

            RepoEquipos.Update(telescopio);
        }
    }
}