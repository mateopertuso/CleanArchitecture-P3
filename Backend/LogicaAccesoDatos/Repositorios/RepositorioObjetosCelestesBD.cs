using LogicaAccesoDatos.EF;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioObjetosCelestesBD : IRepositorioObjetosCelestes
    {
        public StellarMindsContext Contexto { get; set; }

        public RepositorioObjetosCelestesBD(StellarMindsContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(ObjetoCeleste nuevo)
        {
            Contexto.ObjetosCelestes.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<ObjetoCeleste> FindAll()
        {
            return Contexto
                .ObjetosCelestes
                .ToList();
        }

        public ObjetoCeleste? FindById(int id)
        {
            return Contexto
                .ObjetosCelestes
                .FirstOrDefault(o => o.Id == id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ObjetoCeleste obj)
        {
            throw new NotImplementedException();
        }
    }
}