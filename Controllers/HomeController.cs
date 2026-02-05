using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        // Método que se ejecuta cuando visitas /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // Método para página "Acerca de"
        public IActionResult About()
        {
            return View();
        }
    }
}
