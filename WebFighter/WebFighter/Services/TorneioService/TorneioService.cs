using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;
using WebFighter.Repositories.LutadorRepository;
using WebFighter.Services.LutadorService;

namespace WebFighter.Services.TorneioService
{
    public class TorneioService : ITorneioService
    {
        ILutadorRepository _lutadorRepository;
        ILutadorService _lutadorService;
        public TorneioService(ILutadorRepository lutadorRepository, ILutadorService lutadorService)
        {
            _lutadorRepository = lutadorRepository;
            _lutadorService = lutadorService;
        }

        public List<LutadorViewModel> OrganizarPorIdade(List<int> idsLutadores)
        {
            try
            {
                List<LutadorViewModel> lutadoresOrdenadosPorIdade = new List<LutadorViewModel>();
                foreach (int id in idsLutadores)
                {
                    lutadoresOrdenadosPorIdade.Add(_lutadorRepository.BuscarLutadorPorID(id));
                }

                if(lutadoresOrdenadosPorIdade == null || lutadoresOrdenadosPorIdade.Count() == 0)
                {
                    throw new Exception("Erro ao buscar lutadores");
                }

                return lutadoresOrdenadosPorIdade.OrderBy(lutador => lutador.Idade).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar lutadores por idade: " + ex.Message);
            }
        }

        public TorneioViewModel RealizarTorneio(List<LutadorViewModel> lutadoresParticipantes)
        {
            TorneioViewModel torneioRealizado = new TorneioViewModel();
            List<LutadorViewModel> vencedoresDasLutas = lutadoresParticipantes;

            torneioRealizado.ChaveamentoOitavas = vencedoresDasLutas;

            for (int i = 1; i <= 4; i++)
            {
                if (i == 2)
                    torneioRealizado.ChaveamentoQuartas = vencedoresDasLutas;
                else if (i == 3)
                    torneioRealizado.ChaveamentoSemiFinal = vencedoresDasLutas;
                else if (i == 4)
                    torneioRealizado.ChaveamentoFinal = vencedoresDasLutas;

                vencedoresDasLutas = RetornaQualificados(vencedoresDasLutas);
            }

            torneioRealizado.Vencedor = vencedoresDasLutas.FirstOrDefault();

            return torneioRealizado;
        }

        public List<LutadorViewModel> RetornaQualificados(List<LutadorViewModel> lutadores)
        {
            List<LutadorViewModel> qualificados = new List<LutadorViewModel>();

            for (int i = 0; i < lutadores.Count(); i += 2)
            {
                LutadorViewModel vencedorLuta = Batalhar(lutadores[i], lutadores[i + 1]);
                qualificados.Add(vencedorLuta);
            }

            return qualificados;
        }

        public LutadorViewModel Batalhar(LutadorViewModel competidor1, LutadorViewModel competidor2)
        {
            LutadorViewModel vencedorDaBatalha;

            if (ChanceDeVitoria(competidor1) > ChanceDeVitoria(competidor2))
            {
                vencedorDaBatalha = competidor1;
                
            }
            else if (ChanceDeVitoria(competidor1) < ChanceDeVitoria(competidor2))
            {
                vencedorDaBatalha = competidor2;
            }
            else
            {
                vencedorDaBatalha = Desempate(competidor1, competidor2);
            }

            competidor1.Lutas++;
            competidor2.Lutas++;

            if(competidor1.Id == vencedorDaBatalha.Id)
            {
                competidor1.Vitorias++;
                competidor2.Derrotas++;
            }
            else
            {
                competidor2.Vitorias++;
                competidor1.Derrotas++;
            }

            _lutadorRepository.AtualizarLutador(competidor1);
            _lutadorRepository.AtualizarLutador(competidor2);

            return vencedorDaBatalha;
        }

        public int ChanceDeVitoria(LutadorViewModel lutador)
        {
            int chanceVitoria;
            if (lutador.Lutas > 0)
            {
                chanceVitoria = lutador.Vitorias * 100 / lutador.Lutas;
            }
            else
                chanceVitoria = 0;

            return chanceVitoria;
        }

        public LutadorViewModel Desempate(LutadorViewModel lutador1, LutadorViewModel lutador2)
        {
            LutadorViewModel lutadorVencedor = new LutadorViewModel();

            if (lutador1.ArtesMarciais > lutador2.ArtesMarciais)
            {
                lutadorVencedor = lutador1;
            }
            else if (lutador1.ArtesMarciais < lutador2.ArtesMarciais)
            {
                lutadorVencedor = lutador2;
            }
            else
            {
                if (lutador1.Lutas > lutador2.Lutas)
                {
                    lutadorVencedor = lutador1;
                }
                else if (lutador1.Lutas < lutador2.Lutas)
                {
                    lutadorVencedor = lutador2;
                }
                else
                {
                    Random randLutador = new Random();
                    int lutadorEscolhido = randLutador.Next(0, 2);

                    if (lutadorEscolhido == 0)
                        lutadorVencedor = lutador1;
                    else
                        lutadorVencedor = lutador2;
                }
            }

            return lutadorVencedor;
        }
    }
}
