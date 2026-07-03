using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        public ICrearPrestamo CUCrearPrestamo { get; set; }
        public IDevolverPrestamo CUDevolverPrestamo { get; set; }
        public IVerificarDisponibilidadEquipo CUVerificarDisponibilidadEquipo { get; set; }
        public IListadoPrestamosActivosUsuario CUListadoPrestamosActivosUsuario { get; set; }
        public IListadoPrestamosSocioEntreFechas CUListadoPrestamosSocioEntreFechas { get; set; }
        public IListadoSociosPorTelescopio CUListadoSociosPorTelescopio {get; set;}
        public IListadoAuditoriasPrestamos CUListadoAuditoriasPrestamos { get; set; }
        public IBuscarAuditoriaPrestamo CUBuscarAuditoriaPrestamo { get; set; }
        public IBuscarPrestamoPorId CUBuscarPrestamoPorId { get; set; }
        public IListadoPrestamosPorCoordinador CUListadoPrestamosPorCoordinador {get; set;}

        public PrestamosController(ICrearPrestamo cuCrearPrestamo, IVerificarDisponibilidadEquipo cuVerificarDisponibilidadEquipo,
            IDevolverPrestamo cuDevolverPrestamo, IListadoPrestamosActivosUsuario cuListadoPrestamosActivosUsuario, 
            IListadoPrestamosSocioEntreFechas cuListadoPrestamosSocioEntreFechas, IListadoSociosPorTelescopio cuListadoSociosPorTelescopio, 
            IListadoAuditoriasPrestamos cuListadoAuditoriasPrestamos, IBuscarAuditoriaPrestamo cuBuscarAuditoriaPrestamo,
            IBuscarPrestamoPorId cuBuscarPrestamoPorId, IListadoPrestamosPorCoordinador cuListadoPrestamosPorCoordinador)
        {
            CUCrearPrestamo = cuCrearPrestamo;
            CUVerificarDisponibilidadEquipo = cuVerificarDisponibilidadEquipo;
            CUDevolverPrestamo = cuDevolverPrestamo;
            CUListadoPrestamosActivosUsuario = cuListadoPrestamosActivosUsuario;
            CUListadoPrestamosSocioEntreFechas = cuListadoPrestamosSocioEntreFechas;
            CUListadoSociosPorTelescopio = cuListadoSociosPorTelescopio;
            CUListadoAuditoriasPrestamos = cuListadoAuditoriasPrestamos;
            CUBuscarAuditoriaPrestamo = cuBuscarAuditoriaPrestamo;
            CUBuscarPrestamoPorId = cuBuscarPrestamoPorId;
            CUListadoPrestamosPorCoordinador = cuListadoPrestamosPorCoordinador;
        }


        /// <summary>
        /// Registra un nuevo préstamo de equipos astronómicos.
        /// </summary>
        /// <remarks>
        /// Permite a un coordinador generar un préstamo para un socio. El sistema valida disponibilidad de stock y compatibilidad 
        /// entre los equipos seleccionados antes de registrar el préstamo.
        /// </remarks>
        /// <param name="dto">
        /// Datos necesarios para crear el préstamo.
        /// </param>
        /// <response code="200">
        /// Préstamo registrado correctamente.
        /// </response>
        /// <response code="400">
        /// Los datos enviados son inválidos o no cumplen las reglas de negocio.
        /// </response>
        /// <response code="401">
        /// El usuario no se encuentra autenticado.
        /// </response>
        /// <response code="403">
        /// El usuario autenticado no posee permisos para crear préstamos.
        /// </response>
        /// <response code="500">
        /// Ocurrió un error inesperado durante el proceso.
        /// </response>
        [Authorize(Roles = "COORDINADOR")]
        [HttpPost]
        public IActionResult Alta([FromBody] AltaPrestamoDTO? dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("No se enviaron datos");
                }

                PrestamoDTO prestamo = CUCrearPrestamo.Ejecutar(dto);

                return Ok(prestamo);
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

        [HttpGet("disponibilidad/{equipoId:int?}")]
        public IActionResult VerificarDisponibilidad(int? equipoId)
        {
            try
            {
                if (equipoId == null)
                {
                    return BadRequest("No se proporcionó id");
                }

                bool disponible = CUVerificarDisponibilidadEquipo.EstaDisponible(equipoId.Value);

                return Ok(disponible);
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

        [Authorize(Roles = "COORDINADOR")]
        [HttpPut("devolver/{id}")]
        public IActionResult Devolver(int id, int coordinadorId)
        {
            try
            {
                CUDevolverPrestamo.Ejecutar(id, coordinadorId);

                return Ok("Préstamo devuelto correctamente");
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
        [HttpGet("vigentes/usuario/{usuarioId}")]
        public IActionResult ObtenerActivosPorUsuario(int usuarioId)
        {
            try
            {
                var prestamos = CUListadoPrestamosActivosUsuario
                    .ObtenerPorUsuario(usuarioId);

                return Ok(prestamos);
            }
            catch
            {
                return StatusCode(500,"Ocurrió un error inesperado");
            }
        }

        [Authorize]
        [HttpGet("usuario/{usuarioId}/{mes}/{anio}")] public IActionResult ObtenerPrestamosSocio(int usuarioId, int mes, int anio)
        {
            try
            {
                var datos = CUListadoPrestamosSocioEntreFechas.Obtener(usuarioId, mes, anio);
                return Ok(datos);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error");
            }
        }

        [HttpGet("socios/telescopio/{telescopioId}")]
        public IActionResult ObtenerSociosPorTelescopio(int telescopioId)
        {
            try
            {
                var socios = CUListadoSociosPorTelescopio.Obtener(telescopioId);

                return Ok(socios);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");}
        }

        

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("coordinador/{coordinadorId}")]
        public IActionResult ObtenerPorCoordinador(int coordinadorId)
        {
            try
            {
                var prestamos = CUListadoPrestamosPorCoordinador.Obtener(coordinadorId);

                return Ok(prestamos);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }



        [HttpGet("{id}")]
        public IActionResult ObtenerPrestamo(int id)
        {
            try
            {
                return Ok(CUBuscarPrestamoPorId.Obtener(id));
            }
            catch (DatosInvalidosException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{prestamoId}/auditorias")]
        public IActionResult ObtenerAuditoriasPrestamo(int prestamoId)
        {
            try
            {
                var auditorias = CUListadoAuditoriasPrestamos.Obtener(prestamoId);

                return Ok(auditorias);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }
    }
}