using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebFighter.Models;
using WebFighter.Repositories.LutadorRepository;
using WebFighter.Services.LutadorService;
using WebFighter.Services.TorneioService;
using Xunit;

namespace WebFighter.Tests.TorneioTests
{
    public class TorneioTests
    {
        Mock<ILutadorService> mockLutadorService = new Mock<ILutadorService>();
        Mock<ILutadorRepository> mockLutadorRepos = new Mock<ILutadorRepository>();

        [Fact]
        public void PorcentagemVitoria()
        {
            TorneioService torneioService = new TorneioService(mockLutadorRepos.Object, mockLutadorService.Object);

            LutadorViewModel lutador = new LutadorViewModel
            {
                Id = 1,
                Lutas = 10,
                Vitorias = 8,
                Derrotas = 2
            };

            Assert.Equal(80, torneioService.ChanceDeVitoria(lutador));
        }

        [Fact]
        public void DesempateTest()
        {
            TorneioService torneioService = new TorneioService(mockLutadorRepos.Object, mockLutadorService.Object);

            //Por Numero de artes marciais
            LutadorViewModel lutador1 = new LutadorViewModel { Id = 1, Nome = "Lutador 1", Idade = 20, ArtesMarciais = 3, Lutas = 10, Vitorias = 7, Derrotas = 3 };
            LutadorViewModel lutador2 = new LutadorViewModel { Id = 2, Nome = "Lutador 2", Idade = 20, ArtesMarciais = 2, Lutas = 10, Vitorias = 7, Derrotas = 3 };

            LutadorViewModel lutador3 = new LutadorViewModel { Id = 3, Nome = "Lutador 3", Idade = 20, ArtesMarciais = 1, Lutas = 10, Vitorias = 7, Derrotas = 3 };
            LutadorViewModel lutador4 = new LutadorViewModel { Id = 4, Nome = "Lutador 4", Idade = 20, ArtesMarciais = 3, Lutas = 10, Vitorias = 7, Derrotas = 3 };

            LutadorViewModel lutador5 = new LutadorViewModel { Id = 5, Nome = "Lutador 5", Idade = 20, ArtesMarciais = 2, Lutas = 10, Vitorias = 7, Derrotas = 3 };
            LutadorViewModel lutador6 = new LutadorViewModel { Id = 6, Nome = "Lutador 6", Idade = 20, ArtesMarciais = 1, Lutas = 10, Vitorias = 7, Derrotas = 3 };

            Assert.Equal(1, torneioService.Desempate(lutador1, lutador2).Id);
            Assert.Equal(4, torneioService.Desempate(lutador3, lutador4).Id);
            Assert.Equal(5, torneioService.Desempate(lutador5, lutador6).Id);

            //Por numero de lutas
            LutadorViewModel lutador7 = new LutadorViewModel { Id = 7, Nome = "Lutador 7", Idade = 20, ArtesMarciais = 3, Lutas = 12 };
            LutadorViewModel lutador8 = new LutadorViewModel { Id = 8, Nome = "Lutador 8", Idade = 20, ArtesMarciais = 3, Lutas = 10 };

            LutadorViewModel lutador9 = new LutadorViewModel { Id = 9, Nome = "Lutador 9", Idade = 20, ArtesMarciais = 3, Lutas = 7 };
            LutadorViewModel lutador10 = new LutadorViewModel { Id = 10, Nome = "Lutador 10", Idade = 20, ArtesMarciais = 3, Lutas = 1 };

            LutadorViewModel lutador11 = new LutadorViewModel { Id = 11, Nome = "Lutador 11", Idade = 20, ArtesMarciais = 3, Lutas = 5 };
            LutadorViewModel lutador12 = new LutadorViewModel { Id = 12, Nome = "Lutador 12", Idade = 20, ArtesMarciais = 3, Lutas = 0 };

            Assert.Equal(7, torneioService.Desempate(lutador7, lutador8).Id);
            Assert.Equal(9, torneioService.Desempate(lutador9, lutador10).Id);
            Assert.Equal(11, torneioService.Desempate(lutador11, lutador12).Id);

            //Por numero randomico
            LutadorViewModel lutador13 = new LutadorViewModel { Id = 13, Nome = "Lutador 13", Idade = 20, ArtesMarciais = 3, Lutas = 10, Vitorias = 10, Derrotas = 0 };
            LutadorViewModel lutador14 = new LutadorViewModel { Id = 14, Nome = "Lutador 14", Idade = 20, ArtesMarciais = 3, Lutas = 10, Vitorias = 10, Derrotas = 0 };

            Assert.NotNull(torneioService.Desempate(lutador13, lutador14));
        }

        [Fact]
        public void BatalhaTeste()
        {
            TorneioService torneioService = new TorneioService(mockLutadorRepos.Object, mockLutadorService.Object);

            LutadorRepository lutadorRepos = new LutadorRepository();

            var lutador1 = lutadorRepos.BuscarLutadorPorID(1);
            var lutador2 = lutadorRepos.BuscarLutadorPorID(2);

            var lutadorMock1 = lutadorRepos.BuscarLutadorPorID(1);
            var lutadorMock2 = lutadorRepos.BuscarLutadorPorID(2);

            var chance1 = torneioService.ChanceDeVitoria(lutador1);
            var chance2 = torneioService.ChanceDeVitoria(lutador2);

            var checkLutador = new LutadorViewModel();

            if (chance1 > chance2)
                checkLutador = lutador1;
            else if (chance1 < chance2)
                checkLutador = lutador2;
            else
                checkLutador = torneioService.Desempate(lutador1, lutador2);

            var vencedor = torneioService.Batalhar(lutador1, lutador2);

            Assert.Equal(checkLutador.Id, vencedor.Id);

            lutadorRepos.AtualizarLutador(lutadorMock1);
            lutadorRepos.AtualizarLutador(lutadorMock2);

            Assert.Equal(lutadorMock1.Lutas, lutadorRepos.BuscarLutadorPorID(1).Lutas);
            Assert.Equal(lutadorMock1.Vitorias, lutadorRepos.BuscarLutadorPorID(1).Vitorias);
            Assert.Equal(lutadorMock1.Derrotas, lutadorRepos.BuscarLutadorPorID(1).Derrotas);

            Assert.Equal(lutadorMock2.Lutas, lutadorRepos.BuscarLutadorPorID(2).Lutas);
            Assert.Equal(lutadorMock2.Vitorias, lutadorRepos.BuscarLutadorPorID(2).Vitorias);
            Assert.Equal(lutadorMock2.Derrotas, lutadorRepos.BuscarLutadorPorID(2).Derrotas);
        }
    }
}
