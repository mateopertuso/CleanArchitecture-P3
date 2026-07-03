using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUAltaCamara : IAltaCamara
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUAltaCamara(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarAlta(AltaCamaraDTO dto)
        {
            Camara camara = MapperEquipos.ToCamara(dto);

            RepoEquipos.Add(camara);
        }
    }
}