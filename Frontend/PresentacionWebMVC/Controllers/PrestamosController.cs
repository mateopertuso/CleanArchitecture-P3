using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentacionWebMVC.Auxiliar;
using PresentacionWebMVC.Models;
using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Controllers
{
    public class PrestamosController : Controller
    {
        public string URLApiUsuarios { get; set; }
        public string URLApiEquipos { get; set; }
        public string URLApiPrestamos { get; set; }

        public PrestamosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuarios");
                URLApiEquipos = config.GetValue<string>("UrlApiEquipos");
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamos");

            }
            else if (env.IsProduction())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuariosProd");
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposProd");
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosProd");
            }
        }

        public ActionResult Create()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "COORDINADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                CreatePrestamoViewModel vm = new CreatePrestamoViewModel
                    {
                        Prestamo = new AltaPrestamoDTO()
                    };

                CargarListas(vm);

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(new CreatePrestamoViewModel
                    {
                        Prestamo = new AltaPrestamoDTO(),
                        Usuarios = new List<SelectListItem>(),
                        Telescopios = new List<SelectListItem>(),
                        Monturas = new List<SelectListItem>(),
                        Camaras = new List<SelectListItem>(),
                        Oculares = new List<SelectListItem>()
                    }
                );
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePrestamoViewModel vm)
        {
            try
            {
                vm.Prestamo.CoordinadorId = int.Parse(HttpContext.Session.GetString("usuarioId"));

                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiPrestamos, vm.Prestamo, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);

                CargarListas(vm);

                return View(vm);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
                CargarListas(vm);
                return View(vm);
            }
        }


        private void CargarListas(CreatePrestamoViewModel vm)
        {
            var respuestaUsuarios = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiUsuarios);

            var tareaUsuarios = respuestaUsuarios.Content
                .ReadFromJsonAsync<List<UsuarioDTO>>();

            tareaUsuarios.Wait();

            var usuarios = tareaUsuarios.Result;

            var respuestaEquipos = AuxliarClienteHttp
                .EnviarSolicitud("GET", URLApiEquipos);

            var tareaEquipos = respuestaEquipos.Content
                .ReadFromJsonAsync<List<EquipoDTO>>();

            tareaEquipos.Wait();

            var equipos = tareaEquipos.Result;

            vm.Usuarios = usuarios.Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.NombreCompleto
                    });

            vm.Telescopios = equipos
                .Where(e => e.TipoEquipo == "Telescopio")
                .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = $"{e.Marca} {e.Modelo}"
                    });

            vm.Monturas = equipos
                .Where(e => e.TipoEquipo == "Montura")
                .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = $"{e.Marca} {e.Modelo}"
                    });

            vm.Camaras = equipos
                .Where(e => e.TipoEquipo == "Camara")
                .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = $"{e.Marca} {e.Modelo}"
                    });

            vm.Oculares = equipos
                .Where(e => e.TipoEquipo == "Ocular")
                .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = $"{e.Marca} {e.Modelo}"
                    });
        }

        [HttpGet]
        public JsonResult VerificarDisponibilidad(int equipoId)
        {
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud( "GET", $"{URLApiPrestamos}/disponibilidad/{equipoId}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    return Json(false);
                }

                var tarea = respuesta.Content.ReadFromJsonAsync<bool>();

                tarea.Wait();

                return Json(tarea.Result);
            }
            catch
            {
                return Json(false);
            }
        }

        public ActionResult Devolver()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) ||
                rol.ToUpper() != "COORDINADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                CreatePrestamoViewModel vm = new CreatePrestamoViewModel();

                var respuestaUsuarios = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiUsuarios);

                var tareaUsuarios = respuestaUsuarios.Content.ReadFromJsonAsync<List<UsuarioDTO>>();

                tareaUsuarios.Wait();

                var usuarios = tareaUsuarios.Result;

                vm.Usuarios = usuarios.Select(u =>
                        new SelectListItem
                        {
                            Value = u.Id.ToString(),
                            Text = u.NombreCompleto
                        });

                return View(vm);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
                return View();
            }
        }


        [HttpPost]
        public ActionResult DevolverPrestamo(int prestamoId)
        {
            try
            {
                int coordinadorId = int.Parse(HttpContext.Session.GetString("usuarioId"));

                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("PUT", $"{URLApiPrestamos}/devolver/{prestamoId}?coordinadorId={coordinadorId}",
                        null, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Devolver));
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                return RedirectToAction(nameof(Devolver));
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";

                return RedirectToAction(nameof(Devolver));
            }
        }


        [HttpGet]
        //metodo para que el controlador mvc pueda devolver datos consumidos dinamicamente mediante fetch,
        //intermediario entre vista y WebAPI
        public JsonResult ObtenerPrestamosUsuario(int usuarioId)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                var respuesta =
                    AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}/vigentes/usuario/{usuarioId}", null, token);

                if (!respuesta.IsSuccessStatusCode)
                {
                    return Json(new List<PrestamoDTO>());
                }

                var tarea =respuesta.Content
                    .ReadFromJsonAsync<List<PrestamoDTO>>();

                tarea.Wait();

                return Json(tarea.Result);
            }
            catch
            {
                return Json(new List<PrestamoDTO>());
            }
        }

        public ActionResult MisPrestamos()
        {
            PrestamosSocioViewModel vm = new PrestamosSocioViewModel
                {
                    Mes = DateTime.Now.Month,
                    Anio = DateTime.Now.Year
                };

            return View(vm);
        }

        [HttpPost]
        public ActionResult MisPrestamos(PrestamosSocioViewModel vm)
        {
            try
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("usuarioId"));

                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}/usuario/{usuarioId}/{vm.Mes}/{vm.Anio}", null, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<List<PrestamoListadoDTO>>();

                    tarea.Wait();
                    vm.Prestamos = tarea.Result;
                }
                else
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(vm);
            }
        }


        public ActionResult SociosPorTelescopio()
        {
            try
            {
                SociosPorTelescopioViewModel vm = new SociosPorTelescopioViewModel();

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiEquipos);

                var tarea = respuesta.Content
                        .ReadFromJsonAsync<List<EquipoDTO>>();

                tarea.Wait();

                var equipos = tarea.Result;

                vm.Telescopios = equipos
                        .Where(e => e.TipoEquipo == "Telescopio")
                        .Select(e =>
                            new SelectListItem
                            {
                                Value = e.Id.ToString(),
                                Text = $"{e.Marca} {e.Modelo}"
                            });

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                return View(new SociosPorTelescopioViewModel());
            }
        }

        [HttpPost]
        public ActionResult SociosPorTelescopio(SociosPorTelescopioViewModel vm)
        {
            try
            {
                var respuestaEquipos = AuxliarClienteHttp.EnviarSolicitud(
                        "GET",
                        URLApiEquipos
                    );

                var tareaEquipos = respuestaEquipos.Content
                        .ReadFromJsonAsync<List<EquipoDTO>>();

                tareaEquipos.Wait();

                var equipos = tareaEquipos.Result;

                vm.Telescopios = equipos
                        .Where(e => e.TipoEquipo == "Telescopio")
                        .Select(e => new SelectListItem
                            {
                                Value = e.Id.ToString(),
                                Text = $"{e.Marca} {e.Modelo}"
                            });

                var respuestaSocios = AuxliarClienteHttp.EnviarSolicitud(
                        "GET",
                        $"{URLApiPrestamos}/socios/telescopio/{vm.TelescopioId}"
                    );

                if (respuestaSocios.IsSuccessStatusCode)
                {
                    var tareaSocios = respuestaSocios.Content
                            .ReadFromJsonAsync<List<SocioPrestamoDTO>>();

                    tareaSocios.Wait();

                    vm.Socios = tareaSocios.Result;
                }
                else
                {
                    ViewBag.Error = AuxliarClienteHttp
                            .ObtenerError(respuestaSocios);
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();

                return View(vm);
            }
        }


        //metodo priv para cargar coordinadores
        private void CargarCoordinadores(AuditoriasPrestamosViewModel vm)
        {
            string token = HttpContext.Session.GetString("token");

            var respuestaUsuarios = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiUsuarios}/todos", null, token);

            var tareaUsuarios = respuestaUsuarios.Content
                    .ReadFromJsonAsync<List<UsuarioDTO>>();

            tareaUsuarios.Wait();

            var usuarios = tareaUsuarios.Result;

            vm.Coordinadores = usuarios
                .Where(u => u.Rol == "COORDINADOR")
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.NombreCompleto
                });
        }

        [HttpGet]
        public ActionResult AuditoriasPrestamos(int? coordinadorId)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                AuditoriasPrestamosViewModel vm = new AuditoriasPrestamosViewModel
                    {
                        CoordinadorId = coordinadorId
                    };

                CargarCoordinadores(vm);

                if (coordinadorId.HasValue)
                {
                    string token = HttpContext.Session.GetString("token");
                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}/coordinador/{coordinadorId.Value}",null, token);

                    var tarea = respuesta.Content.ReadFromJsonAsync<List<PrestamoDTO>>();

                    tarea.Wait();
                    vm.Prestamos = tarea.Result;
                }

                return View(vm);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";

                return View(new AuditoriasPrestamosViewModel());
            }
        }


        [HttpGet]
        public ActionResult AuditoriaPrestamo(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}/{id}/auditorias", null, token);

                if (!respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);

                    return View( new List<AuditoriaPrestamoDTO>());
                }

                var tarea = respuesta.Content.ReadFromJsonAsync<List<AuditoriaPrestamoDTO>>();

                tarea.Wait();

                ViewBag.PrestamoId = id;

                return View(tarea.Result);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";

                return View(new List<AuditoriaPrestamoDTO>());
            }
        }


        [HttpGet]
        public ActionResult DetallePrestamo(int id)
        {
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}/{id}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);

                    return View();
                }

                var tarea = respuesta.Content.ReadFromJsonAsync<PrestamoDTO>();

                tarea.Wait();

                return View(tarea.Result);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";

                return View();
            }
        }

    }
}