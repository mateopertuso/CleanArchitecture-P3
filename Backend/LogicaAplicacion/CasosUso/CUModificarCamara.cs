using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUModificarCamara : IModificarCamara
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUModificarCamara(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarModificacion(ModificarCamaraDTO dto)
        {
            Camara camara = MapperEquipos.ToCamara(dto);

            RepoEquipos.Update(camara);
        }
    }
}