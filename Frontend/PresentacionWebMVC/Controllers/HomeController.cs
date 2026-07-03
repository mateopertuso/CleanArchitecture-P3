using Microsoft.AspNetCore.Mvc;
using PresentacionWebMVC.Models;
using System.Diagnostics;

namespace PresentacionWebMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("rol");
            var user = HttpContext.Session.GetString("username");
            ViewBag.Rol = rol;
            ViewBag.Username = user;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
