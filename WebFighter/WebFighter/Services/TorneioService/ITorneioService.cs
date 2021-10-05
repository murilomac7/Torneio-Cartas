using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;

namespace WebFighter.Services.TorneioService
{
    public interface ITorneioService
    {
        public TorneioViewModel RealizarTorneio(List<LutadorViewModel> lutadoresParticipantes);
        public List<LutadorViewModel> OrganizarPorIdade(List<int> idsLutadores);
    }
}
