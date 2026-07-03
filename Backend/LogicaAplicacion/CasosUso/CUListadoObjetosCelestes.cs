using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUListadoObjetosCelestes : IListadoObjetosCelestes
    {
        private readonly IRepositorioObjetosCelestes _repoObjetos;

        public CUListadoObjetosCelestes(IRepositorioObjetosCelestes repoObjetos)
        {
            _repoObjetos = repoObjetos;
        }

        public IEnumerable<ObjetoCelesteDTO> ObtenerTodos()
        {
            return _repoObjetos
                .FindAll()
                .Select(MapperObjetosCelestes.ToDTO);
        }
    }
}