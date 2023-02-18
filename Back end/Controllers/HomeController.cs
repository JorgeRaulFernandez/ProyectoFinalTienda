using Microsoft.AspNetCore.Mvc;

namespace ProyectoTienda.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
