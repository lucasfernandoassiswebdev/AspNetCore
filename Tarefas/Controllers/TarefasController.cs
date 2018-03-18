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

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gerenciar lista de tarefas";

            //obter itens da tarefa
            return View(new TarefaViewModel
            {
                TarefaItens = await _tarefaItemService.GetItemAsync()
            });
        }

        [HttpGet]
        public IActionResult AdicionarItemTarefa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItemTarefa([Bind("Id,EstaCompleta,Nome,DataConclusao")] TarefaItem tarefa)
        {
            //Este Bind informa quais campos o DataBind deve vincular, só será permitido a injeção na tarefa desses 4 campos
            //O Bind é uma maneira de restringir, proteger.
            if(ModelState.IsValid){
                await _tarefaItemService.AdicionarItemAsync(tarefa);
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }
    }
}