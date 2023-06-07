using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Banco_de_Dados.Models
{
    public class Jogador
    {
        [Key]
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        
        [ForeignKey("Equipe")] //(nome da classe que provém a chave estrangeira) - DATA ANNOTATION IdEquipe
        public int IdEquipe { get; set; }
        public Equipe Equipe {get;set;}
    }
}