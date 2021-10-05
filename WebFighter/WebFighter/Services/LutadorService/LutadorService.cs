using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;
using WebFighter.Repositories.LutadorRepository;

namespace WebFighter.Services.LutadorService
{
    public class LutadorService : ILutadorService
    {
        ILutadorRepository _lutadorRepository;
        public LutadorService(ILutadorRepository lutadorRepository)
        {
            _lutadorRepository = lutadorRepository;
        }
        public void AtualizarLutador(LutadorViewModel lutador)
        {
            try
            {
                _lutadorRepository.AtualizarLutador(lutador);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar lutador: " + ex.Message);
            }
        }

        public LutadorViewModel BuscarLutadorPorID(int id)
        {
            try
            {
                return _lutadorRepository.BuscarLutadorPorID(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar lutador: " + ex.Message);
            }
        }

        public List<LutadorViewModel> ListarLutadores()
        {
            try
            {
                List<LutadorViewModel> listaLutadores = _lutadorRepository.ListarLutadores();

                return listaLutadores;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar lutadores: " + ex.Message);
            }
        }
    }
}
