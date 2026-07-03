
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentacionWebMVC.Auxiliar;
using PresentacionWebMVC.Models;
using PresentacionWebMVC.Models.DTOs;
using System.Net;

namespace PresentacionWebMVC.Controllers
{
    public class ObservacionesController : Controller
    {
        public string URLApiObservaciones { get; set; }
        public string URLApiPrestamos { get; set; }
        public string URLApiObjetosCelestes { get; set; }

        public ObservacionesController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiObservaciones = config.GetValue<string>("UrlApiObservaciones");
                URLApiObjetosCelestes = config.GetValue<string>("UrlApiObjetosCelestes");
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamos");

            }
            else if (env.IsProduction())
            {
                URLApiObservaciones = config.GetValue<string>("UrlApiObservacionesProd");
                URLApiObjetosCelestes = config.GetValue<string>("UrlApiObjetosCelestesProd");
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosProd");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "SOCIO")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                string token = HttpContext.Session.GetString("token");

                int usuarioId = int.Parse(HttpContext.Session.GetString("usuarioId"));

                var respuestaPrestamos = AuxliarClienteHttp
                    .EnviarSolicitud("GET", $"{URLApiPrestamos}/vigentes/usuario/{usuarioId}",null, token);

                var tareaPrestamos = respuestaPrestamos.Content
                    .ReadFromJsonAsync<List<PrestamoDTO>>();

                tareaPrestamos.Wait();

                var prestamos = tareaPrestamos.Result;

                var respuestaObjetos = AuxliarClienteHttp
                    .EnviarSolicitud("GET", URLApiObjetosCelestes, null, token);

                var tareaObjetos = respuestaObjetos.Content
                    .ReadFromJsonAsync<List<ObjetoCelesteDTO>>();

                tareaObjetos.Wait();

                var objetos = tareaObjetos.Result;

                CreateObservacionViewModel vm = new CreateObservacionViewModel
                    {
                        Observacion = new AltaObservacionDTO()
                    };

                vm.Prestamos = prestamos.Select(p =>
                        new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = $"Préstamo #{p.Id} - {p.Telescopio}"
                        });

                vm.ObjetosCelestes = objetos.Select(o =>
                        new SelectListItem
                        {
                            Value = o.Id.ToString(),
                            Text = $"{o.Nombre} ({o.Tipo})"
                        });

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(
                    new CreateObservacionViewModel
                    {
                        Observacion = new AltaObservacionDTO(),

                        Prestamos = new List<SelectListItem>(),

                        ObjetosCelestes = new List<SelectListItem>()
                    }
                );
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateObservacionViewModel vm)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiObservaciones, vm.Observacion, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (respuesta.StatusCode == HttpStatusCode.Unauthorized || 
                    respuesta.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(vm);
            }
        }

        [HttpPost]
        //metodo para devolver json a js
        public JsonResult Evaluar([FromBody] EvaluarObservacionRequestDTO dto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiObservaciones}/evaluar", dto, token);

                if (!respuesta.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        ok = false
                    });
                }

                var tarea = respuesta.Content.ReadFromJsonAsync<EvaluacionObservacionDTO>();

                tarea.Wait();

                return Json(tarea.Result);
            }
            catch
            {
                return Json(new
                {
                    ok = false
                });
            }
        }


        [HttpGet]
        public ActionResult Ranking()
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiObservaciones}/ranking", null, token);

                if (!respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    return View(new List<RankingObjetoCelesteDTO>());
                }

                var tarea = respuesta.Content.ReadFromJsonAsync<List<PresentacionWebMVC.Models.DTOs.RankingObjetoCelesteDTO>>();

                tarea.Wait();
                return View(tarea.Result);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
                return View(new List<RankingObjetoCelesteDTO>());
            }
        }
    }
}
