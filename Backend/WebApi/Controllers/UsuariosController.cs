using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebApi.JWT;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public ILogin CULogin { get; set; }
        public IAltaUsuario CUAltaUsuario { get; set; }
        public IListadoUsuarios CUListadoUsuarios { get; set; }
        public IListadoUsuariosCompleto CUListadoUsuariosCompleto { get; set; }

        public UsuariosController(ILogin cuLogin, IAltaUsuario cuAltaUsuario, IListadoUsuarios cuListadoUsuarios,
            IListadoUsuariosCompleto cuListadoUsuariosCompleto)
        {
            CULogin = cuLogin;
            CUAltaUsuario = cuAltaUsuario;
            CUListadoUsuarios = cuListadoUsuarios;
            CUListadoUsuariosCompleto = cuListadoUsuariosCompleto;
        }



        /// <summary>
        /// Permite autenticar un usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Valida las credenciales ingresadas y, si son correctas, genera un token JWT que será utilizado para acceder a los endpoints protegidos de la API.
        /// </remarks>
        /// <param name="dto">
        /// Datos de acceso del usuario (username y password).
        /// </param>
        /// <response code="200">
        /// Login exitoso. Retorna información del usuario y el token JWT.
        /// </response>
        /// <response code="400">
        /// No se enviaron datos o faltan credenciales obligatorias.
        /// </response>
        /// <response code="401">
        /// Las credenciales son incorrectas.
        /// </response>
        /// <response code="500">
        /// Ocurrió un error inesperado.
        /// </response>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO? dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("No se proporcionaron datos");
                }

                if (string.IsNullOrWhiteSpace(dto.Username))
                {
                    return BadRequest("El username es requerido");
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return BadRequest("La contraseña es requerida");
                }

                UsuarioDTO usuario = CULogin.EjecutarLogin(dto.Username, dto.Password);

                if (usuario == null)
                {
                    return Unauthorized("Credenciales incorrectas");
                }

                string token = ManejadorJWT.GenerarToken(usuario);

                return Ok(new
                {
                    UsuarioId = usuario.Id,
                    Rol = usuario.Rol,
                    Username = usuario.Username,
                    Token = token
                });
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado, intente de nuevo");
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout correcto");
        }



        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite crear un usuario indicando sus datos personales, credenciales y rol. Solo puede ser ejecutado por usuarios con rol ADMINISTRADOR.
        /// </remarks>
        /// <param name="dto">
        /// Datos necesarios para registrar el usuario.
        /// </param>
        /// <response code="200">
        /// Usuario creado correctamente.
        /// </response>
        /// <response code="400">
        /// Los datos enviados son inválidos o incompletos.
        /// </response>
        /// <response code="401">
        /// El usuario no se encuentra autenticado.
        /// </response>
        /// <response code="403">
        /// El usuario autenticado no posee permisos para realizar el alta.
        /// </response>
        /// <response code="500">
        /// Ocurrió un error inesperado durante el proceso.
        /// </response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult AltaUsuario([FromBody] AltaUsuarioDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se proporcionan datos");

                UsuarioDTO usuario = CUAltaUsuario.EjecutarAlta(dto);

                return Ok(usuario);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500,
                    "Ocurrió un problema, no se pudo realizar el alta");
            }
        }

        /// <summary>
        /// Obtiene el listado de usuarios con rol Socio.
        /// </summary>
        /// <remarks>
        /// Utilizado principalmente en el RF09 para consultar los socios
        /// a los que se les prestó un telescopio determinado.
        /// No devuelve administradores ni coordinadores.
        /// </remarks>
        /// <response code="200">Listado obtenido correctamente.</response>
        /// <response code="500">Ocurrió un error inesperado.</response>
        [HttpGet]
        public IActionResult TraerTodos()
        {
            try
            {
                IEnumerable<UsuarioDTO> usuarios = CUListadoUsuarios.ObtenerListado();

                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }


        [Authorize(Roles = "ADMINISTRADOR,COORDINADOR")]
        [HttpGet("todos")]
        public IActionResult TraerUsuarios()
        {
            try
            {
                return Ok(CUListadoUsuariosCompleto.ObtenerListado());
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

    }
}