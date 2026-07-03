using CasosUso.InterfacesCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetosCelestesController : ControllerBase
    {
        public IListadoObjetosCelestes CUListadoObjetos { get; set; }

        public ObjetosCelestesController(IListadoObjetosCelestes cuListado)
        {
            CUListadoObjetos = cuListado;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(CUListadoObjetos.ObtenerTodos());
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }
    }
}
