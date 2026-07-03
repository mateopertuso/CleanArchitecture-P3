using Excepciones;
using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuariosBD : IRepositorioUsuarios
    {
        public StellarMindsContext Contexto { get; set; }

        public RepositorioUsuariosBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Usuario nuevo)
        {
            nuevo.Validar();
            Contexto.Usuarios.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios
                .Include(u => u.Rol)
                .ToList();
        }

        public Usuario? FindById(int id)
        {
            return Contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario? Login(string username, string password)
        {
            return Contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u =>
                    u.Username == username &&
                    u.Password.Valor == password
                );
        }

        public void Remove(int id)
        {
            Usuario? aBorrar = FindById(id);

            if (aBorrar == null) throw new OperacionInvalidaException("No existe el usuario con id: " + id);

            Contexto.Usuarios.Remove(aBorrar);
            Contexto.SaveChanges();
        }

        public void Update(Usuario obj)
        {
            obj.Validar();

            Usuario? existente = Contexto.Usuarios
                                      .AsNoTracking()
                                      .Where(u => u.Id == obj.Id)
                                      .SingleOrDefault();

            if (existente == null) throw new OperacionInvalidaException("No existe el usuario");

            Contexto.Usuarios.Update(obj);
            Contexto.SaveChanges();
        }
    }
}