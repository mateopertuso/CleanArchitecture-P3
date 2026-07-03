using CasosUso.DTOs;
using CasosUso.DTOs.Editar_equipos;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUModificarMontura : IModificarMontura
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUModificarMontura(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarModificacion(ModificarMonturaDTO dto)
        {
            Montura montura = MapperEquipos.ToMontura(dto);

            RepoEquipos.Update(montura);
        }
    }
}