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
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")] //http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            //variável que armazena as equipes listadas do banco de dados 
            ViewBag.Equipe = c.Equipe.ToList();

            //retorna a view de equipe (TELA)
            return View();
        }

       [Route("Cadastrar")] //http://localhost/Equipe/Cadastrar
        public IActionResult Cadastrar(IFormCollection form)
        {
            //Instância do objeti equipe
            Equipe novaEquipe = new Equipe();

            //atribuição de valores recebidos do formulário                 
            novaEquipe.Nome = form["Nome"].ToString();
            // novaEquipe.Imagem = form["Imagem"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                
                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                novaEquipe.Imagem = file.FileName;

            }

            else
            {
                novaEquipe.Imagem = "padrão.png";
            }

            //adiciona objeto na tabela BD
            c.Equipe.Add(novaEquipe);

            //salva altereações no BD
            c.SaveChanges();

            //
            ViewBag.Equipe = c.Equipe.ToList();

            return LocalRedirect("~/Equipe/Listar");
            
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
          Equipe equipeBuscada = c.Equipe.First(e => e.IdEquipe == id);

          c.Remove(equipeBuscada);

          c.SaveChanges();

          return LocalRedirect("~/Equipe/Listar");

        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
          Equipe equipe = c.Equipe.First(x => x.IdEquipe == id);

          ViewBag.Equipe = equipe;


            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
          Equipe equipe = new Equipe();

          equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

          equipe.Nome = form["Nome"].ToString();

          if (form.Files.Count > 0)
          {
            var file = form.Files[0];

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var path = Path.Combine(folder,file.Name);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            equipe.Imagem = file.FileName;

          }
          else
            {
                equipe.Imagem = "padrão.png";
            }

            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == equipe.IdEquipe);
            equipeBuscada.Nome = equipe.Nome;
            equipeBuscada.Imagem = equipe.Imagem;
            c.Equipe.Update(equipeBuscada);
            c.SaveChanges();
            return LocalRedirect("~/Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        
    }
}