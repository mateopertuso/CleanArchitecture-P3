using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Models
{
    public class PrestamosSocioViewModel
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public List<PrestamoListadoDTO> Prestamos {get; set;} = new List<PrestamoListadoDTO>();
    }
}