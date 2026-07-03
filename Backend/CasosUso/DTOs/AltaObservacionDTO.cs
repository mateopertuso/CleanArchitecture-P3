using LogicaNegocio.Enums;

public class AltaObservacionDTO
{
    public int PrestamoId { get; set; }
    public int ObjetoCelesteId { get; set; }
    public DateTime Fecha { get; set; }
    public IndicadorEvaluacion Indicador { get; set; }
    public string? Detalle { get; set; }
}