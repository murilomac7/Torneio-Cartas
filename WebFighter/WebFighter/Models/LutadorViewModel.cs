using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFighter.Models
{
    [Table("Lutadores")]
    public class LutadorViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int ArtesMarciais { get; set; }
        public int Lutas { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
    }
}
