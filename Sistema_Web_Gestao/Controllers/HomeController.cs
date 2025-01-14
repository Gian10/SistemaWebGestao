using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Gestao.Models;
using System.Diagnostics;

namespace Sistema_Web_Gestao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login"); 
            }
            return View();
        }

       
       
    }
}
