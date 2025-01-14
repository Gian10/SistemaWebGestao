using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sistema_Web_Gestao.Data;
using Sistema_Web_Gestao.Helper;
using Sistema_Web_Gestao.Models;
using Sistema_Web_Gestao.Services;

namespace Sistema_Web_Gestao.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }
                var result = await _usuarioService.GetAllUsuariosAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao listar dados.",
                    Details = ex.Message
                });
            }
        }

        // GET: Usuarios/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                return View(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu erro ao exibir a tela.",
                    Details = ex.Message
                });
            }
        }

        // GET: Usuarios/Create
        [Route("Create")]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao exibir a tela.",
                    Details = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(usuario.FotoUsuario))
                {
                    try
                    {
                        usuario.FotoUsuario = await SalvarImagemAsync(usuario.FotoUsuario);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new
                        {
                            Message = "Erro ao processar a imagem.",
                            Details = ex.Message
                        });
                    }
                }
                // Foi usada criptografia para senha 
                usuario.Senha = Useful.Encrypt(usuario.Senha);
                await _usuarioService.AddUsuarioAsync(usuario);

                return Ok(new { success = true, message = "Usuário cadastrado com sucesso", userId = usuario.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao cadastrar usuário.",
                    Details = ex.Message
                });
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                return View(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao exibir a tela.",
                    Details = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!string.IsNullOrEmpty(usuario.FotoUsuario))
                {
                    try
                    {
                        var imagemProcessada = await SalvarImagemAsync(usuario.FotoUsuario);
                        if (string.IsNullOrEmpty(imagemProcessada))
                        {
                            return StatusCode(500, new
                            {
                                Message = "Erro ao processar a imagem.",
                                Details = "O processamento da imagem retornou um valor inválido."
                            });
                        }
                        usuario.FotoUsuario = imagemProcessada;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new
                        {
                            Message = "Erro ao processar a imagem.",
                            Details = ex.Message
                        });
                    }
                }
                // Foi usada criptografia para senha 
                usuario.Senha = Useful.Encrypt(usuario.Senha);
                await _usuarioService.UpdateUsuarioAsync(usuario);
                return Ok(new
                {
                    Message = "Usuário editado com sucesso.",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao editar o Usuário.",
                    Details = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
                return Ok(new
                {
                    Message = "Usuário deletado!.",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao deletar dado.",
                    Details = ex.Message
                });
            }
        }

        // Criar nome para a foto e salvar no diretorio
        private async Task<string> SalvarImagemAsync(string base64Image)
        {
            var arquivoNome = $"{Guid.NewGuid()}.jpg";
            var PathIncial = Path.Combine("wwwroot", "imagens", arquivoNome);
            var PathCompleto = Path.Combine(Directory.GetCurrentDirectory(), PathIncial);

            byte[] arqBytes = Convert.FromBase64String(base64Image);
            await System.IO.File.WriteAllBytesAsync(PathCompleto, arqBytes);

            return arquivoNome;
        }
    }
}
