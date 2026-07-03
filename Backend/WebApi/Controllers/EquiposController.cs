using CasosUso.DTOs;
using CasosUso.DTOs.Editar_equipos;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        public IAltaTelescopio CUAltaTelescopio { get; set; }
        public IAltaMontura CUAltaMontura { get; set; }
        public IAltaCamara CUAltaCamara { get; set; }
        public IAltaOcular CUAltaOcular { get; set; }
        public IListadoEquipos CUListadoEquipos { get; set; }
        public IBajaEquipo CUBajaEquipo { get; set; }
        public IModificarTelescopio CUModificarTelescopio { get; set; }
        public IModificarMontura CUModificarMontura { get; set; }
        public IModificarCamara CUModificarCamara { get; set; }
        public IModificarOcular CUModificarOcular { get; set; }
        public IBuscarEquipoPorId CUBuscarEquipoPorId { get; set; }
        public IBuscarTelescopioPorId CUBuscarTelescopioPorId { get; set; }
        public IBuscarMonturaPorId CUBuscarMonturaPorId { get; set; }
        public IBuscarCamaraPorId CUBuscarCamaraPorId { get; set; }
        public IBuscarOcularPorId CUBuscarOcularPorId { get; set; }

        public EquiposController(IAltaTelescopio cuAltaTelescopio, IAltaMontura cuAltaMontura, 
            IAltaCamara cuAltaCamara, IAltaOcular cuAltaOcular, IListadoEquipos cuListadoEquipos,
            IBajaEquipo cuBajaEquipo, IModificarTelescopio cuModificarTelescopio, IModificarMontura cuModificarMontura,
            IModificarCamara cuModificarCamara, IModificarOcular cuModificarOcular, IBuscarEquipoPorId cuBuscarEquipoPorId,
            IBuscarTelescopioPorId cuBuscarTelescopioPorId, IBuscarMonturaPorId cuBuscarMonturaPorId,
            IBuscarCamaraPorId cuBuscarCamaraPorId, IBuscarOcularPorId cuBuscarOcularPorId)
        {
            CUAltaTelescopio = cuAltaTelescopio;
            CUAltaMontura = cuAltaMontura;
            CUAltaCamara = cuAltaCamara;
            CUAltaOcular = cuAltaOcular;
            CUListadoEquipos = cuListadoEquipos;
            CUBajaEquipo = cuBajaEquipo;
            CUModificarTelescopio = cuModificarTelescopio;
            CUModificarMontura = cuModificarMontura;
            CUModificarCamara = cuModificarCamara;
            CUModificarOcular = cuModificarOcular;
            CUBuscarEquipoPorId = cuBuscarEquipoPorId;
            CUBuscarTelescopioPorId = cuBuscarTelescopioPorId;
            CUBuscarMonturaPorId = cuBuscarMonturaPorId;
            CUBuscarCamaraPorId = cuBuscarCamaraPorId;
            CUBuscarOcularPorId = cuBuscarOcularPorId;
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost("telescopio")]
        public IActionResult AltaTelescopio([FromBody] AltaTelescopioDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se proporcionaron datos para el alta");

                CUAltaTelescopio.EjecutarAlta(dto);

                return Ok("Telescopio creado correctamente");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error y no se pudo crear el telescopio");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost("montura")]
        public IActionResult AltaMontura([FromBody] AltaMonturaDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se proporcionaron datos para el alta");

                CUAltaMontura.EjecutarAlta(dto);

                return Ok("Montura creada correctamente");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error y no se pudo crear la montura");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost("camara")]
        public IActionResult AltaCamara([FromBody] AltaCamaraDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se proporcionaron datos para el alta");

                CUAltaCamara.EjecutarAlta(dto);

                return Ok("Cámara creada correctamente");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error y no se pudo crear la cámara");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost("ocular")]
        public IActionResult AltaOcular([FromBody] AltaOcularDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se proporcionaron datos para el alta");

                CUAltaOcular.EjecutarAlta(dto);

                return Ok("Ocular creado correctamente");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error y no se pudo crear el ocular");
            }
        }

        [HttpGet]
        public IActionResult TraerTodos()
        {
            try
            {
                IEnumerable<EquipoDTO> equipos = CUListadoEquipos.ObtenerListado();

                return Ok(equipos);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al obtener los equipos");
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id:int?}")]
        public IActionResult Eliminar(int? id)
        {
            try
            {
                if (id == null) return BadRequest("No se proporcionó el id");

                if (id <= 0) return BadRequest("El id debe ser mayor a cero");

                CUBajaEquipo.EjecutarBaja(id.Value);

                return NoContent();
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(
                    500,
                    "Ocurrió un error al eliminar el equipo"
                );
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("telescopio/{id:int?}")]
        public IActionResult ModificarTelescopio(int? id, [FromBody] ModificarTelescopioDTO? dto)
        {
            try
            {
                if (id == null)
                    return BadRequest("No se proporcionó id");

                if (dto == null)
                    return BadRequest("No se proporcionaron datos");

                if (id != dto.Id)
                    return BadRequest("Los ids no coinciden");

                CUModificarTelescopio.EjecutarModificacion(dto);

                return Ok(dto);
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un problema y no se pudo realizar la modificación.");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("montura/{id:int?}")]
        public IActionResult ModificarMontura(int? id, [FromBody] ModificarMonturaDTO? dto)
        {
            try
            {
                if (id == null)
                    return BadRequest("No se proporcionó id");

                if (dto == null)
                    return BadRequest("No se proporcionaron datos");

                if (id != dto.Id)
                    return BadRequest("Los ids no coinciden");

                CUModificarMontura.EjecutarModificacion(dto);

                return Ok(dto);
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al modificar la montura");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("camara/{id:int?}")]
        public IActionResult ModificarCamara(int? id, [FromBody] ModificarCamaraDTO? dto)
        {
            try
            {
                if (id == null) return BadRequest("No se proporcionó id");

                if (dto == null) return BadRequest("No se proporcionaron datos");

                if (id != dto.Id) return BadRequest("Los ids no coinciden");

                CUModificarCamara.EjecutarModificacion(dto);

                return Ok(dto);
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al modificar la cámara");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("ocular/{id:int?}")]
        public IActionResult ModificarOcular(int? id, [FromBody] ModificarOcularDTO? dto)
        {
            try
            {
                if (id == null) return BadRequest("No se proporcionó id");

                if (dto == null) return BadRequest("No se proporcionaron datos");

                if (id != dto.Id) return BadRequest("Los ids no coinciden");

                CUModificarOcular.EjecutarModificacion(dto);

                return Ok(dto);
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al modificar el ocular");
            }
        }


        [HttpGet("{id:int?}")]
        public IActionResult BuscarPorId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                EquipoDTO equipo = CUBuscarEquipoPorId.BuscarPorId(id.Value);

                return Ok(equipo);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [HttpGet("telescopio/{id:int?}")]
        public IActionResult BuscarTelescopioPorId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                TelescopioDTO telescopio = CUBuscarTelescopioPorId.BuscarPorId(id.Value);

                return Ok(telescopio);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [HttpGet("montura/{id:int?}")]
        public IActionResult BuscarMonturaPorId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                MonturaDTO montura = CUBuscarMonturaPorId.BuscarPorId(id.Value);

                return Ok(montura);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [HttpGet("camara/{id:int?}")]
        public IActionResult BuscarCamaraPorId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                CamaraDTO camara = CUBuscarCamaraPorId.BuscarPorId(id.Value);

                return Ok(camara);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [HttpGet("ocular/{id:int?}")]
        public IActionResult BuscarOcularPorId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                OcularDTO ocular = CUBuscarOcularPorId.BuscarPorId(id.Value);

                return Ok(ocular);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }
    }
}