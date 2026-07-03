using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentacionWebMVC.Models
{
    public class CreateObservacionViewModel
    {
        public AltaObservacionDTO Observacion { get; set; }
        public IEnumerable<SelectListItem> Prestamos { get; set; }
        public IEnumerable<SelectListItem> ObjetosCelestes { get; set; }
        public string Indicador { get; set; }
        public string? Detalle { get; set; }
    }
}