using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUAltaUsuario : IAltaUsuario
    {
        private IRepositorioUsuarios _repoUsuarios;
        private IRepositorioRoles _repoRoles;

        public CUAltaUsuario(IRepositorioUsuarios repoUsuarios, IRepositorioRoles repoRoles)
        {
            _repoUsuarios = repoUsuarios;
            _repoRoles = repoRoles;
        }

        public UsuarioDTO EjecutarAlta(AltaUsuarioDTO dto)
        {
            // 1 buscar rol en repositorio
            var rol = _repoRoles.FindByNombre(dto.Rol);

            if (rol == null)
                throw new DatosInvalidosException("Rol inválido");

            // 2 mapear DTO -> entidad
            Usuario usuario = MapperUsuarios.ToUsuario(dto, rol);

            // 3 guardar
            _repoUsuarios.Add(usuario);

            // 4 devolver DTO
            return MapperUsuarios.ToDto(usuario);
        }
    }
}