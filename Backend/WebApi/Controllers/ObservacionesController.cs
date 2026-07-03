using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObservacionesController : ControllerBase
    {
        public IEvaluarObservacion CUEvaluarObservacion {get; set;}
        public ICrearObservacion CUCrearObservacion {get; set;}
        public IRankingObjetosCelestes CURankingObjetosCelestes {get; set;}

        public ObservacionesController(IEvaluarObservacion cuEvaluar, ICrearObservacion cuCrearObservacion, IRankingObjetosCelestes cuRankingObjetosCelestes)
        {
            CUEvaluarObservacion = cuEvaluar;
            CUCrearObservacion = cuCrearObservacion;
            CURankingObjetosCelestes = cuRankingObjetosCelestes;
        }

        [Authorize(Roles = "SOCIO")]
        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] EvaluarObservacionRequestDTO dto)
        {
            try
            {
                var resultado = CUEvaluarObservacion
                    .Evaluar(dto.PrestamoId, dto.ObjetoCelesteId);

                return Ok(resultado);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500,"Ocurrió un error inesperado");
            }
        }

        [Authorize(Roles = "SOCIO")]
        [HttpPost]
        public IActionResult Post([FromBody] AltaObservacionDTO dto)
        {
            try
            {
                CUCrearObservacion.Crear(dto);
                return Ok();
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        [Authorize]
        [HttpGet("ranking")]
        public IActionResult Ranking()
        {
            try
            {
                var ranking = CURankingObjetosCelestes.ObtenerRanking();
                return Ok(ranking);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

    }
}
