using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Apresentacao.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult EmailReset()
        {
            return View();
        }

        public IActionResult NovaSenha()
        {
            return View();
        }

    }
}