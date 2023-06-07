using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_Banco_de_Dados.Models;

namespace Projeto_Banco_de_Dados.Infra
{
    public class Context : DbContext
    {
        public Context()
        {
            
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured)
           {
            //string de conexão com o banco
            //Data Source : o nome do servidor do gerenciador do banco
            //initial catalog : nome do banco de dados

            //Autentificação pelo windows
            //Intregated Security : Autentificação
            //
            
            // Autentiicação pelo SqlServer
            //user Id = "nome do seu usuario de login"
            // pwd = "senha do seu usuario"
            optionsBuilder.UseSqlServer("Data Source = NOTE15-S14; Initial Catalog = gamerManha; User Id = sa; pwd = Senai@134 ; TrustServerCertificate = true");
           }
        }
        public DbSet<Jogador> Jogador{get;set;}
        public DbSet<Equipe> Equipe {get;set;}
    }
}