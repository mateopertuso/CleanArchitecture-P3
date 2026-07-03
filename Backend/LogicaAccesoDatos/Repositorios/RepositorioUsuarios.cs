//using LogicaNegocio.ClasesDominio;
//using LogicaNegocio.InterfacesRepositorio;
//using LogicaNegocio.ValueObjects;
//using System.Collections.Generic;
//using System.Linq;

//namespace LogicaAccesoDatos.Repositorios
//{
//    public class RepositorioUsuarios : IRepositorioUsuarios
//    {
//        private static List<Usuario> _usuarios = new List<Usuario>();

//        public RepositorioUsuarios()
//        {
//            if (!_usuarios.Any())
//            {
//                _usuarios.Add(new Usuario
//                {
//                    Id = 1,
//                    NombreCompleto = "Admin Test",
//                    Direccion = "Canelones",
//                    Telefono = "099999999",
//                    Email = new Email("admin@test.com"),
//                    Username = "userAdmin",
//                    Password = new Password("Probando123."),
//                    Rol = new Rol
//                    {
//                        Id = 1,
//                        Nombre = "ADMINISTRADOR"
//                    }
//                });
//            }
//        }

//        public Usuario? Login(string Username, string password)
//        {
//            return _usuarios.FirstOrDefault(u =>
//                u.Username == Username&&
//                u.Password.Valor == password
//            );
//        }

//        public void Add(Usuario nuevo)
//        {
//            nuevo.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;
//            _usuarios.Add(nuevo);
//        }

//        public void Remove(int id)
//        {
//            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
//            if (usuario != null)
//                _usuarios.Remove(usuario);
//        }

//        public void Update(Usuario nuevo)
//        {
//            var usuario = _usuarios.FirstOrDefault(u => u.Id == nuevo.Id);
//            if (usuario != null)
//            {
//                usuario.NombreCompleto = nuevo.NombreCompleto;
//                usuario.Direccion = nuevo.Direccion;
//                usuario.Telefono = nuevo.Telefono;
//                usuario.Email = nuevo.Email;
//                usuario.Username = nuevo.Username;
//                usuario.Password = nuevo.Password;
//                usuario.Rol = nuevo.Rol;
//            }
//        }

//        public Usuario? FindById(int id)
//        {
//            return _usuarios.FirstOrDefault(u => u.Id == id);
//        }

//        public IEnumerable<Usuario> FindAll()
//        {
//            return _usuarios;
//        }
//    }
//}