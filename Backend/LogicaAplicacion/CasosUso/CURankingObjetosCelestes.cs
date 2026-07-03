using LogicaNegocio.InterfacesRepositorio;

public class CURankingObjetosCelestes : IRankingObjetosCelestes
{
    private readonly IRepositorioObservaciones _repoObservaciones;

    public CURankingObjetosCelestes(IRepositorioObservaciones repoObservaciones)
    {
        _repoObservaciones = repoObservaciones;
    }

    public IEnumerable<RankingObjetoCelesteDTO> ObtenerRanking()
    {
        return _repoObservaciones
            .FindAll()
            .GroupBy(o => new
            {
                o.ObjetoCeleste.Nombre,
                o.ObjetoCeleste.Tipo
            })
            .Select(g => new RankingObjetoCelesteDTO
            {
                Nombre = g.Key.Nombre,
                Tipo = g.Key.Tipo.ToString(),
                CantidadObservaciones = g.Count()
            })
            .OrderByDescending(x => x.CantidadObservaciones);
    }
}