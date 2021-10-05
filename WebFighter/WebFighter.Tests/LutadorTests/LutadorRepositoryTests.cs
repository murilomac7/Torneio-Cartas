using System;
using System.Collections.Generic;
using System.Text;
using WebFighter.Models;
using WebFighter.Repositories.LutadorRepository;
using Xunit;

namespace WebFighter.Tests.LutadorTests
{
    public class LutadorRepositoryTests
    {
        [Fact]
        public void GetLutadorPorIdTest()
        {
            var lutadorRepos = new LutadorRepository();

            var lutadorSelecionado = lutadorRepos.BuscarLutadorPorID(24);

            Assert.NotNull(lutadorSelecionado);
            Assert.Equal("All Might", lutadorSelecionado.Nome);
            Assert.Equal(43, lutadorSelecionado.Idade);
            Assert.Equal(3, lutadorSelecionado.ArtesMarciais);
        }

        [Fact]
        public void ListarLutadoresTest()
        {
            var lutadorRepos = new LutadorRepository();

            List<LutadorViewModel> lutadores = lutadorRepos.ListarLutadores();

            Assert.NotEmpty(lutadores);
            Assert.Equal(32, lutadores.Count);
        }

        [Fact]
        public void AtualizarLutadorTest()
        {
            var lutadorRepos = new LutadorRepository();

            var lutadorParaAtualizar = lutadorRepos.BuscarLutadorPorID(24);
            var lutadorMocado = lutadorRepos.BuscarLutadorPorID(24);

            Assert.NotNull(lutadorParaAtualizar);
            Assert.NotNull(lutadorMocado);

            lutadorParaAtualizar.Lutas++;
            lutadorParaAtualizar.Vitorias++;
            lutadorParaAtualizar.Derrotas++;

            lutadorRepos.AtualizarLutador(lutadorParaAtualizar);

            Assert.Equal(lutadorParaAtualizar.Lutas, lutadorMocado.Lutas + 1);
            Assert.Equal(lutadorParaAtualizar.Vitorias, lutadorMocado.Vitorias + 1);
            Assert.Equal(lutadorParaAtualizar.Derrotas, lutadorMocado.Derrotas + 1);

            lutadorParaAtualizar.Lutas--;
            lutadorParaAtualizar.Vitorias--;
            lutadorParaAtualizar.Derrotas--;

            lutadorRepos.AtualizarLutador(lutadorParaAtualizar);

            Assert.Equal(lutadorParaAtualizar.Lutas, lutadorRepos.BuscarLutadorPorID(24).Lutas);
            Assert.Equal(lutadorParaAtualizar.Vitorias, lutadorRepos.BuscarLutadorPorID(24).Vitorias);
            Assert.Equal(lutadorParaAtualizar.Derrotas, lutadorRepos.BuscarLutadorPorID(24).Derrotas);
        }
    }
}
