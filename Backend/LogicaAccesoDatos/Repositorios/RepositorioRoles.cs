//using LogicaNegocio.ClasesDominio;
//using LogicaNegocio.InterfacesRepositorio;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace LogicaAccesoDatos.Repositorios
//{
//    public class RepositorioRoles : IRepositorioRoles
//    {
//        private static List<Rol> _roles = new List<Rol>
//        {
//            new Rol { Id = 1, Nombre = "ADMINISTRADOR" },
//            new Rol { Id = 2, Nombre = "SOCIO" },
//            new Rol { Id = 3, Nombre = "COORDINADOR" }
//        };

//        public Rol? FindByNombre(string nombre)
//        {
//            return _roles.FirstOrDefault(r => r.Nombre == nombre);
//        }

//        public IEnumerable<Rol> FindAll()
//        {
//            return _roles;
//        }
//    }
//}
