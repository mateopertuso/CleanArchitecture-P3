
using Microsoft.AspNetCore.Mvc;
using PresentacionWebMVC.Auxiliar;
using PresentacionWebMVC.Models.DTOs;

namespace PresentacionWebMVC.Controllers
{
    public class EquiposController : Controller
    {
        public string URLApiEquipos { get; set; }

        public EquiposController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiEquipos = config.GetValue<string>("UrlApiEquipos");

            }
            else if (env.IsProduction())
            {
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposProd");
            }
        }


        public ActionResult Index()
        {
            List<EquipoDTO> equipos = new List<EquipoDTO>();

            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiEquipos);

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea2 = respuesta.Content
                        .ReadFromJsonAsync<List<EquipoDTO>>();

                    tarea2.Wait();

                    if (tarea2.Result != null)
                    {
                        equipos = tarea2.Result;
                    }
                }
                else
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                }
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View(equipos);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult CreateTelescopio()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTelescopio(AltaTelescopioDTO dto)
        {
            if (ModelState.IsValid) //solo llamo a la api si los datos ya pasaron las validaciones del lado MVC
            {
                try
                {
                    string? token = HttpContext.Session.GetString("token");

                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiEquipos}/telescopio", dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }
                }
                catch
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
            }

            return View(dto);
        }


        public ActionResult CreateMontura()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMontura(AltaMonturaDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string? token = HttpContext.Session.GetString("token");

                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiEquipos}/montura", dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }
                }
                catch
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
            }

            return View(dto);
        }


        public ActionResult CreateCamara()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCamara(AltaCamaraDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string? token = HttpContext.Session.GetString("token");

                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiEquipos}/camara", dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }
                }
                catch
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
            }

            return View(dto);
        }


        public ActionResult CreateOcular()
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOcular(AltaOcularDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string? token = HttpContext.Session.GetString("token");

                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiEquipos}/ocular", dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }
                }
                catch
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
            }

            return View(dto);
        }


        public ActionResult EditTelescopio(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiEquipos}/telescopio/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<TelescopioDTO>();

                    tarea.Wait();
                    return View(tarea.Result);
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTelescopio(int id, ModificarTelescopioDTO dto)
        {
            try
            {
                string? token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("PUT", $"{URLApiEquipos}/telescopio/{id}", dto, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error y no se realizó la modificación.";
            }

            return View(dto);
        }


        public ActionResult EditMontura(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiEquipos}/montura/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<MonturaDTO>();

                    tarea.Wait();
                    return View(tarea.Result);
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMontura(int id, ModificarMonturaDTO dto)
        {
            try
            {
                string? token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("PUT", $"{URLApiEquipos}/montura/{id}", dto, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }
            return View(dto);
        }


        public ActionResult EditCamara(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiEquipos}/camara/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<CamaraDTO>();

                    tarea.Wait();

                    return View(tarea.Result);
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCamara(int id, ModificarCamaraDTO dto)
        {
            try
            {
                string? token = HttpContext.Session.GetString("token");
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("PUT", $"{URLApiEquipos}/camara/{id}", dto, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View(dto);
        }


        public ActionResult EditOcular(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiEquipos}/ocular/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<OcularDTO>();

                    tarea.Wait();

                    return View(tarea.Result);
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOcular(int id, ModificarOcularDTO dto)
        {
            try
            {
                string? token = HttpContext.Session.GetString("token");
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("PUT", $"{URLApiEquipos}/ocular/{id}", dto, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error inesperado";
            }

            return View(dto);
        }


        public ActionResult Delete(int id)
        {
            string? rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(rol) || rol.ToUpper() != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiEquipos}/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea = respuesta.Content.ReadFromJsonAsync<EquipoDTO>();

                    tarea.Wait();
                    return View(tarea.Result);
                }

                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }
            catch
            {
                ViewBag.Error = "Ocurrió un problema inesperado";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string? token = HttpContext.Session.GetString("token");

                var respuesta = AuxliarClienteHttp.EnviarSolicitud("DELETE", $"{URLApiEquipos}/{id}", null, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                        respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        return RedirectToAction("Login", "Usuarios");
                    }

                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                }
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error y no fue posible realizar el borrado";
            }

            return View();
        }
    }
}