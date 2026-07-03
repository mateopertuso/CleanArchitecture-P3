
using Microsoft.AspNetCore.Mvc;
using PresentacionWebMVC.Models.DTOs;
using PresentacionWebMVC.Auxiliar;

namespace PresentacionWebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        public string URLApiUsuarios { get; set; }
        public string URLApiLogin { get; set; }

        public UsuariosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuarios");
                URLApiLogin = config.GetValue<string>("UrlApiLogin");

            }
            else if (env.IsProduction())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuariosProd");
                URLApiLogin = config.GetValue<string>("UrlApiLoginProd");
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiLogin, dto);

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea2 = respuesta.Content.ReadFromJsonAsync<LoginRespuestaDTO>();
                    tarea2.Wait();

                    LoginRespuestaDTO usuario = tarea2.Result;
                    HttpContext.Session.SetString("rol", usuario.Rol);
                    HttpContext.Session.SetString("token", usuario.Token);
                    HttpContext.Session.SetString("username", usuario.Username);
                    HttpContext.Session.SetString("usuarioId", usuario.UsuarioId.ToString() ?? "");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                }
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error. Intente de nuevo más tarde";
            }

            return View(dto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        //RF02 Crear nuevos usuarios
        public ActionResult Create() // solo administradores
        {
            string? rol = ObtenerRolUsuarioLogueado();
            if (string.IsNullOrEmpty(rol) || rol?.ToUpper() != "ADMINISTRADOR") return RedirectToAction("Login", "Usuarios");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AltaUsuarioDTO dto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                if (!ModelState.IsValid) return View(dto);
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiUsuarios, dto, token);

                if (!respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    return View(dto);
                }

                return RedirectToAction("Login", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewBag.Error =ex.Message;
                return View(dto);
            }
        }


        private string? ObtenerRolUsuarioLogueado()
        {
            return HttpContext.Session.GetString("rol");
        }
    }
}