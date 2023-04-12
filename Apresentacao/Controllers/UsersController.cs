using Apresentacao.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("Users/Logar")]
        public IActionResult Logar(LogarModel LogarModel)
        {

            Console.WriteLine("Entrou aqui");

            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Users/CriarUsuario")]
        public async Task<IActionResult> CriarUsuario(UserModel entity)
        {
            try
            {
                return Ok(entity);
            }
            catch (Exception)
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpPost]
        [Route("Users/EnviarReset/{id}")]
        public IActionResult EnviarReset(string id)
        {
            try
            {
                return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest(string.Empty);
            }
        }

        public IActionResult EmailReset()
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.Token = "AQUIFICAOTOKEN";
            return View(tokenModel);
        }

        [HttpPost("NovaSenha/{token}")]
        public IActionResult NovaSenha([FromQuery] string Token)
        {
            return View();
        }

        public IActionResult VeficarEmail()
        {
            return View();
        }
    }
}