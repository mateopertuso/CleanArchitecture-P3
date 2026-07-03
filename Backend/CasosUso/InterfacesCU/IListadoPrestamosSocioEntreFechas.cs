using CasosUso.DTOs;

public interface IListadoPrestamosSocioEntreFechas
{
    List<PrestamoListadoDTO> Obtener(int usuarioId, int mes, int anio);
}