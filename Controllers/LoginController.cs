using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Banco_de_Dados.Models;
using Projeto_Banco_de_Dados.Infra;

namespace Projeto_Banco_de_Dados.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        Context c = new Context();

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            string email = form ["Email"].ToString();
            string senha = form["Senha"].ToString();

            Jogador jogadorBuscado = c.Jogador.First(j => j.Email == email && j.Senha == senha);

            //Aqui precisamos implementar a sessão
            
            if (jogadorBuscado != null)
            {
               HttpContext.Session.SetString("UserName",jogadorBuscado.Nome);
               return LocalRedirect("~/"); 
            }
            
            return LocalRedirect("~/Login/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}