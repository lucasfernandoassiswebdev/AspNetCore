using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Models;
using Tarefas.Services;

namespace Tarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ITarefaItemService _tarefaItemService;

        public TarefasController(ITarefaItemService tarefaItemService)
        {
            _tarefaItemService = tarefaItemService;
        }

        public async Task<IActionResult> Index(){
            ViewData["Title"] = "Gerenciar lista de tarefas";

            //obter itens da tarefa
            return View(new TarefaViewModel{
                TarefaItens  = await _tarefaItemService.GetItemAsync()
            });
        }
    }
}