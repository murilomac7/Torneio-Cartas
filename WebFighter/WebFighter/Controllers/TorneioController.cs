using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebFighter.Models;
using WebFighter.Services.LutadorService;
using WebFighter.Services.TorneioService;

namespace WebFighter.Controllers
{
    public class TorneioController : Controller
    {
        ILutadorService _lutadorService;
        ITorneioService _torneioService;

        public static List<TorneioViewModel> torneiosRealizados = new List<TorneioViewModel>();

        public TorneioController(ILutadorService lutadorService, ITorneioService torneioService)
        {
            _lutadorService = lutadorService;
            _torneioService = torneioService;
        }
        public IActionResult Index()
        {
            return View(_lutadorService.ListarLutadores());
        }

        [HttpPost]
        public JsonResult IniciarTorneio(string lutadoresSelecionados)
        {
            List<int> idsLutadores = new List<int>();            

            try
            {
                foreach (var id in lutadoresSelecionados.Split(','))
                {
                    idsLutadores.Add(Convert.ToInt32(id));
                }

                TorneioViewModel torneioRealizado = _torneioService.RealizarTorneio(_torneioService.OrganizarPorIdade(idsLutadores));

                if(torneiosRealizados.Count() == 0)
                {
                    torneioRealizado.Id = 1;
                }
                else
                {
                    torneioRealizado.Id = torneiosRealizados.Count() + 1;
                }

                torneiosRealizados.Add(torneioRealizado);

                return Json(new { Url = $"Torneio/Resultado/{torneioRealizado.Id}" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public IActionResult Resultado(int id)
        {
            try
            {
                TorneioViewModel torneio = torneiosRealizados.Find(t => t.Id == id);

                if(torneio == null)
                {
                    return RedirectToAction("Index");
                }

                return View(torneio);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
