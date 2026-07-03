using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUAltaMontura : IAltaMontura
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUAltaMontura(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarAlta(AltaMonturaDTO dto)
        {
            Montura montura = MapperEquipos.ToMontura(dto);

            RepoEquipos.Add(montura);
        }
    }
}