using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;

namespace WebFighter.Services.LutadorService
{
    public interface ILutadorService
    {
        public List<LutadorViewModel> ListarLutadores();
        public void AtualizarLutador(LutadorViewModel lutador);
        public LutadorViewModel BuscarLutadorPorID(int id);
    }
}
