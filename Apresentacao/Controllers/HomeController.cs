using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
