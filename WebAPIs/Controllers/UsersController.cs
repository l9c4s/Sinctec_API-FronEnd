using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using WebAPIs.Email;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login loginModel)
        {
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Senha))
            {
                return Unauthorized();
            }

            var resultado = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var userCurrent = await _userManager.FindByEmailAsync(loginModel.Email);
                var IdUser = userCurrent.Id;

                var token = new ToeknJWTBuilder()
                                .AddSecuriryKey(JwtSecurityKey.Create("Secret_key-12345678"))
                                .AddSubject("sinctec")
                                .Addissuer("Teste.sinctec.bearer")
                                .Addaudience("Teste.sinctec.bearer")
                                .AddClaims("IdUsuario", IdUser)
                                .AddExpiry(5)
                                .Builder();
                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuarioIdentity")]
        public async Task<IActionResult> AdicionatUsarioIdentity([FromBody] Login loginModel)
        {
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Senha) || string.IsNullOrWhiteSpace(loginModel.cpf))
            {
                return BadRequest("Falta alguns dados");
            }

            var User = new ApplicationUser
            {
                UserName = loginModel.Email,
                Email = loginModel.Email,
                CPF = loginModel.cpf,
                Tipo = TipoUsuario.Comum,
            };

            var Resultado = await _userManager.CreateAsync(User, loginModel.Senha);

            if (Resultado.Errors.Any())
            {
                return Ok(Resultado.Errors);
            }

            if (Resultado.Succeeded)
            {
                var ConfirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(User);
                var EncodeEmailToken = Encoding.UTF8.GetBytes(ConfirmEmailToken);
                var ValidEmailToken = WebEncoders.Base64UrlEncode(EncodeEmailToken);

                string url = $"https://localhost:7066/api/ConfirmarEmailIdentity?UserId={User.Id}&Token={ValidEmailToken}";
                SengridServices Sengrid = new SengridServices();

                var boolEmail = await Sengrid.Execute(User.Email, url, 1);
                if (boolEmail ==  true)
                {
                    return Ok("Usuario Criado com sucesso, Por favor verifique sua caixa de email");
                }

                await _userManager.DeleteAsync(User);
                return BadRequest("Por favor, Tente Novamente");
            }
            return BadRequest("Por favor, Tente Novamente");
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/ConfirmarEmailIdentity")]
        public async Task<IActionResult> ConfirmarEmailIdentity([FromBody] ConfirmEmailMode confirmEmailMode)
        {
            if(confirmEmailMode == null)
            {
                return BadRequest("Preencha Todos os Paramentros");
            }

            if (string.IsNullOrWhiteSpace(confirmEmailMode.UserId) || string.IsNullOrWhiteSpace(confirmEmailMode.Token))
            {
                return NotFound("Preencha Todos os Paramentros");
            }

            var DecodedToken = WebEncoders.Base64UrlDecode(confirmEmailMode.Token);
            string NormaliietTOKEN = Encoding.UTF8.GetString(DecodedToken);

            var user = await _userManager.FindByIdAsync(confirmEmailMode.UserId);
            if (user == null)
            {
                return BadRequest("Usuario não encontrado");
            }

            var result = await _userManager.ConfirmEmailAsync(user , NormaliietTOKEN);
            if (result.Succeeded)
            {
                return Ok("Email Confirmado com sucesso!!");
            }

            return BadRequest("Erro ao tentar confirmar Email");
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/EmailResetDeSenha")]
        public async Task<IActionResult> EmailResetDeSenha(string EmailUser)
        {
            if (string.IsNullOrWhiteSpace(EmailUser))
            {
                return NotFound("Preencha Todos os Paramentros");
            }

            var user = await _userManager.FindByEmailAsync(EmailUser);
            if (user == null)
            {
                return BadRequest("Usuario não encontrado");
            }

            var result = await _userManager.GeneratePasswordResetTokenAsync(user);

            var EncodeResetPassToken = Encoding.UTF8.GetBytes(result);
            var ValidResetPassToken = WebEncoders.Base64UrlEncode(EncodeResetPassToken);


            string url = $"https://localhost:7085/NovaSenha/api/ResetarSenhaIdentity?EmailUser={user.Email}&Token={ValidResetPassToken}";
            SengridServices Sengrid = new SengridServices();

            if(await Sengrid.Execute(user.Email, url, 2))
            {
                return Ok("Por favor, Verifique sua caixa de Email");
            }

            return BadRequest("Erro ao tentar confirmar Email");
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/ResetarSenhaIdentity")]
        public async Task<IActionResult> ResetarSenhaIdentity([FromBody] ResetEmailModel resetEmailModel)
        {
            if (resetEmailModel == null)
            {
                return BadRequest("Preencha todos os Paramentros");
            }

            if (string.IsNullOrWhiteSpace(resetEmailModel.EmailUser) || string.IsNullOrWhiteSpace(resetEmailModel.Senha) || string.IsNullOrWhiteSpace(resetEmailModel.Token))
            {
                return NotFound("Preencha Todos os Paramentros");
            }

            var user = await _userManager.FindByEmailAsync(resetEmailModel.EmailUser);
            if (user == null)
            {
                return BadRequest("Usuario não encontrado");
            }

            var DecodedToken = WebEncoders.Base64UrlDecode(resetEmailModel.Token);
            string NormalitedToken = Encoding.UTF8.GetString(DecodedToken);

            var result = await _userManager.ResetPasswordAsync(user, NormalitedToken, resetEmailModel.Senha);

            if (result.Succeeded)
            {return Ok("Senha alterada!!!");
            }else{
                return BadRequest(result.Errors);
            }
        }


    }




}
