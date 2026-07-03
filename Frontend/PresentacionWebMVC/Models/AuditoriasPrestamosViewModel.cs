using Microsoft.AspNetCore.Mvc.Rendering;
using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Models
{
    public class AuditoriasPrestamosViewModel
    {
        public int? CoordinadorId { get; set; }
        public IEnumerable<SelectListItem> Coordinadores { get; set; } = new List<SelectListItem>();
        public IEnumerable<PrestamoDTO> Prestamos { get; set; } = new List<PrestamoDTO>();
    }
}