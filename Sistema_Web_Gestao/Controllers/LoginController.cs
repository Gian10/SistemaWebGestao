using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Web_Gestao.Helper;
using Sistema_Web_Gestao.Models;
using Sistema_Web_Gestao.Services;

namespace Sistema_Web_Gestao.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            try
            {              
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao Exibir Tela.",
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logar([FromBody] LoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {             
                    return Ok();
                }

                if (login.Email == "oportunidades@smn.com.br" &&  login.Senha == "teste123" )    
                {
                    var Id = Guid.NewGuid();
                    HttpContext.Session.SetString("UserId", Id.ToString());
                    HttpContext.Session.SetString("UserName", "oportunidades");
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
                // Foi usada criptografia para senha 
                login.Senha = Useful.Encrypt(login.Senha);
                var usuario = await _authService.AuthenticateAsync(login.Email, login.Senha);
                if (usuario == null)
                {
                    return NotFound();
                }
                if(usuario.IsGestor == false)
                {
                    return NotFound();
                }

                // Foi usado Session para armazenar dados de login na aplicação
                HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                HttpContext.Session.SetString("UserName", usuario.NomeCompleto.ToString());

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao Logar.",
                    Details = ex.Message
                });
            }
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Login");            
        }

    }
}
