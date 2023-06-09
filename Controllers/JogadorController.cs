using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Banco_de_Dados.Infra;
using Projeto_Banco_de_Dados.Models;

namespace Projeto_Banco_de_Dados.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")] //http://localhost/Jogador/Listar
        public IActionResult Index()
        {
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();

            return View();
        }

        [Route("Cadastrar")] //http://localhost/Jogador/Cadastrar

        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();

            novoJogador.Email = form["Email"].ToString();

            novoJogador.Senha = form["Senha"].ToString();

            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            c.Jogador.Add(novoJogador);

            c.SaveChanges();


            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")] //http://localhost/Jogador/Excluir

        public IActionResult Excluir(int id)
        {
            Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

            c.Jogador.Remove(jogador);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");

        }

        [Route("Editar/{id}")]

        public IActionResult Editar(int id)
        {
            Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

            ViewBag.Jogador = jogador;

            ViewBag.Equipe = c.Equipe.ToList();

            return View("Edit");
        }

        [Route("Atualizar")]

        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.IdJogador = int.Parse(form["Nome"].ToString());
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            Jogador jogadorBuscado = c.Jogador.First(x => x.IdJogador == novoJogador.IdJogador);

            jogadorBuscado.IdJogador = novoJogador.IdJogador;
            jogadorBuscado.Nome = novoJogador.Nome;
            jogadorBuscado.Email = novoJogador.Email;
            jogadorBuscado.Senha= novoJogador.Senha;
            jogadorBuscado.IdEquipe = novoJogador.IdEquipe;
            

            c.Jogador.Update(jogadorBuscado);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");

        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }



}