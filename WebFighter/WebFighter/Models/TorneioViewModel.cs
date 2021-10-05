using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFighter.Models
{
    public class TorneioViewModel
    {
        public int Id { get; set; }
        public LutadorViewModel Vencedor { get; set; }
        public List<LutadorViewModel> ChaveamentoOitavas { get; set; }
        public List<LutadorViewModel> ChaveamentoQuartas { get; set; }
        public List<LutadorViewModel> ChaveamentoSemiFinal { get; set; }
        public List<LutadorViewModel> ChaveamentoFinal { get; set; }
    }
}
