using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto_Banco_de_Dados.Models
{
    public class Equipe
    {
        [Key]//DATA ANNOTATION - IdEquipe
        public int IdEquipe { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Imagem { get; set; }

        // REFERÊNCIA QUE A CLASSE EQUIPE VAI TER ACESSO 
        // A COLLECTION "JOGADOR"
        public ICollection<Jogador> Jogador { get; set; }

    }
}