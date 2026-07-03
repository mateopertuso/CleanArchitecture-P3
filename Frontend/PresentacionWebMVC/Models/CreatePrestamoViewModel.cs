using Microsoft.AspNetCore.Mvc.Rendering;
using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Models
{
    public class CreatePrestamoViewModel
    {
        public AltaPrestamoDTO Prestamo { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<SelectListItem> Telescopios { get; set; }
        public IEnumerable<SelectListItem> Monturas { get; set; }
        public IEnumerable<SelectListItem> Camaras { get; set; }
        public IEnumerable<SelectListItem> Oculares { get; set; }
    }
}