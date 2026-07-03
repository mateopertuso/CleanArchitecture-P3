using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioRolesBD : IRepositorioRoles
    {
        public StellarMindsContext Contexto { get; set; }

        public RepositorioRolesBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public IEnumerable<Rol> FindAll()
        {
            return Contexto.Roles.ToList();
        }

        public Rol? FindByNombre(string nombre)
        {
            return Contexto.Roles
                .FirstOrDefault(r => r.Nombre == nombre);
        }
    }
}