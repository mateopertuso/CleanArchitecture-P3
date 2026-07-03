using LogicaNegocio.ClasesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioRoles
    {
        Rol? FindByNombre(string nombre);
        IEnumerable<Rol> FindAll();
    }
}
