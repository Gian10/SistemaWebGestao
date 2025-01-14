using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_Web_Gestao.Data;
using Sistema_Web_Gestao.Helper;
using Sistema_Web_Gestao.Models;
using Sistema_Web_Gestao.Services;

namespace Sistema_Web_Gestao.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ITaferaService _tarefaService;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly SendEmail _sendEmail;
        public TarefasController(ITaferaService tarefaService, IConfiguration configuration, IUsuarioService usuarioService, SendEmail sendEmail)
        {
            _tarefaService = tarefaService;
            _configuration = configuration;
            _usuarioService = usuarioService;
            _sendEmail = sendEmail;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }
                var tarefas = await _tarefaService.GetAllTarefasWithIncludesAsync(u => u.Usuario);
                var result = tarefas.Where(u => u.Usuario.IsGestor == false).ToList();
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


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
                var usuario = await _usuarioService.GetUsuarioByIdAsync(tarefa.UsuarioId);
                ViewBag.UsuarioNome = usuario.NomeCompleto;
                return View(tarefa);
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


        public IActionResult Create()
        {
            var usuarios = _usuarioService.GetAllUsuariosAsync().Result
                     .Where(u => u.IsGestor == false)
                     .Select(u => new SelectListItem
                     {
                         Value = u.Id.ToString(),
                         Text = u.NomeCompleto
                     })
                     .ToList();

            ViewBag.UsuarioId = usuarios;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _tarefaService.AddTarefaAsync(tarefa);
                    var usuario = _usuarioService.GetUsuarioByIdAsync(tarefa.UsuarioId);
                    // Foi usado Mailtrap para disparar e receber emails
                    // Só dispara email para o subordinado
                    _sendEmail.Send("webgestao@gmail.com", "Web Gestão", usuario.Result.Email, "Nova Tarefa Criada", tarefa.Mensagem);
                    return Ok(new
                    {
                        Message = "Tarefa cadastrada com sucesso.",
                    });
                }
                return View();
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


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
                var usuario = await _usuarioService.GetUsuarioByIdAsync(tarefa.UsuarioId);
                ViewBag.UsuarioNome = usuario.NomeCompleto;
                return View(tarefa);
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


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tarefaService.UpdateTarefaAsync(tarefa);
                    return Ok(new
                    {
                        Message = "Tarefa editado com sucesso.",

                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        Message = "Ocorreu um erro ao editar dado.",
                        Details = ex.Message
                    });
                }
            }
            return NotFound("Erro ao tentar Editar Tarefa!");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tarefaService.DeleteTarefaAsync(id);
                return Ok(new
                {
                    Message = "Tarefa deletada!.",
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

    }
}
