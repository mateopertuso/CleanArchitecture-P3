using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;

namespace LogicaNegocio.ClasesDominio
{

    public class Observacion : IValidable
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public Prestamo Prestamo { get; set; }
        public ObjetoCeleste ObjetoCeleste { get; set; }

        public IndicadorEvaluacion Indicador { get; set; }
        public string? Detalle { get; set; }

        public void Validar()
        {
            if (Prestamo == null)
                throw new Exception("Debe existir un préstamo");

            if (!Prestamo.EstaVigente())
                throw new Exception("El préstamo no está vigente");

            if (ObjetoCeleste == null)
                throw new Exception("Debe seleccionar un objeto celeste");
        }
    }
}
