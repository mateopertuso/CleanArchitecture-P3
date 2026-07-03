using LogicaNegocio.ClasesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuarios : IRepositorio<Usuario>
    {
        Usuario? Login(string email, string password);
    }
}
