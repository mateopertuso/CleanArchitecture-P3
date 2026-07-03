using Excepciones;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

public class CUCrearObservacion : ICrearObservacion
{
    public IRepositorioObservaciones _repoObservaciones { get; set; }
    public IRepositorioPrestamos _repoPrestamos { get; set; }
    public IRepositorioObjetosCelestes _repoObjetos { get; set; }

    public CUCrearObservacion(IRepositorioObservaciones repoObs, IRepositorioPrestamos repoPrest,
        IRepositorioObjetosCelestes repoObj)
    {
        _repoObservaciones = repoObs;
        _repoPrestamos = repoPrest; 
        _repoObjetos = repoObj;
    }

    public void Crear(AltaObservacionDTO dto)
    {
        if (_repoObservaciones.ExisteObservacion(dto.Fecha, dto.PrestamoId, dto.ObjetoCelesteId))
        {
            throw new DatosInvalidosException("Ya existe una observación para ese equipo y objeto celeste en la fecha indicada");
        }

        Prestamo prestamo = _repoPrestamos.FindById(dto.PrestamoId);
        ObjetoCeleste objeto = _repoObjetos.FindById(dto.ObjetoCelesteId);

        Observacion observacion = new Observacion
            {
                Fecha = dto.Fecha,
                Prestamo = prestamo,
                ObjetoCeleste = objeto,
                Indicador = dto.Indicador,
                Detalle = dto.Detalle
            };

        observacion.Validar();

        _repoObservaciones.Add(observacion);
    }
}