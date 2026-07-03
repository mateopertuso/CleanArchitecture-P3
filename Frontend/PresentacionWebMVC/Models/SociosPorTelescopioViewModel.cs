using Microsoft.AspNetCore.Mvc.Rendering;
using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Models
{
    public class SociosPorTelescopioViewModel
    {
        public int TelescopioId { get; set; }
        public IEnumerable<SelectListItem> Telescopios {get; set;} = new List<SelectListItem>();
        public IEnumerable<SocioPrestamoDTO> Socios {get; set;} = new List<SocioPrestamoDTO>();
    }
}