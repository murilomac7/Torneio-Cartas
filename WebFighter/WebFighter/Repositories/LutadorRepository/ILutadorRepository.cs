using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;

namespace WebFighter.Repositories.LutadorRepository
{
    public interface ILutadorRepository
    {
        public List<LutadorViewModel> ListarLutadores();
        public LutadorViewModel BuscarLutadorPorID(int id);
        public bool AtualizarLutador(LutadorViewModel lutador);
    }
}
