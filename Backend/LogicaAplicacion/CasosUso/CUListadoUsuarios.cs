using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUListadoUsuarios : IListadoUsuarios
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }

        public CUListadoUsuarios(IRepositorioUsuarios repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        public IEnumerable<UsuarioDTO> ObtenerListado()
        {
            return RepoUsuarios
                .FindAll()
                .Where(u => u.Rol.Nombre == "SOCIO")
                .Select(MapperUsuarios.ToDto);
        }
    }
}