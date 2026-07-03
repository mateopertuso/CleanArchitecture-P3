using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.CasosUso
{
    public class CULogin : ILogin
    {
        private IRepositorioUsuarios _repoUsuarios;

        public CULogin(IRepositorioUsuarios repoUsuarios)
        {
            _repoUsuarios = repoUsuarios;
        }

        public UsuarioDTO EjecutarLogin(string username, string password)
        {
            Usuario usu = _repoUsuarios.Login(username, password);
            UsuarioDTO dto = MapperUsuarios.ToDto(usu);
            return dto;
        }



    }
}