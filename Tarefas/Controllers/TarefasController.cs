using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Models;
using Tarefas.Services;

namespace Tarefas.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly ITarefaItemService _tarefaItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TarefasController(ITarefaItemService tarefaItemService, UserManager<ApplicationUser> userManager)
        {
            _tarefaItemService = tarefaItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(bool? criterio)
        {
            ViewData["Title"] = "Gerenciar lista de tarefas";

            //Pegando os dados do usuário logado atual  
            var currentUser = await _userManager.GetUserAsync(User);
            //Challenge redireciona para a página de login novamente
            if(currentUser == null)
                return Challenge();
            
            //obter itens da tarefa
            return View(new TarefaViewModel
            {
                
                TarefaItens = await _tarefaItemService.GetItemAsync(criterio, currentUser)
            });
        }

        [HttpGet]
        public IActionResult AdicionarItemTarefa() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarItemTarefa([Bind("Id,EstaCompleta,Nome,DataConclusao")] TarefaItem tarefa)
        {
            //Este Bind informa quais campos o DataBind deve vincular, só será permitido a injeção na tarefa desses 4 campos
            //O Bind é uma maneira de restringir, proteger.
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if(currentUser == null)
                    return Challenge();

                tarefa.OwnerId = currentUser.Id;
                await _tarefaItemService.AdicionarItemAsync(tarefa);
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        //GET tarefas/delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tarefaItem = _tarefaItemService.GetTarefaById(id);
            if (tarefaItem == null)
                return NotFound();

            return View(tarefaItem);
        }

        //Esta Ation name é uma espécie de rota, será levada em conta o Nome "Delete"
        //Ao chamar o método ao invés do nome "DeleteConfirmed"
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tarefaItemService.DeletarItem(id);
            return RedirectToAction(nameof(Index));
        }

        //GET Tarefas/Edit/10
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tarefaItem = _tarefaItemService.GetTarefaById(id);
            if (tarefaItem == null)
                return NotFound();

            return View(tarefaItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstaCompleta,Nome,DataConclusao")] TarefaItem tarefaItem)
        {
            if (id != tarefaItem.Id)
                return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null)
                return Challenge();

            if (ModelState.IsValid)
            {
                try
                {
                    await _tarefaItemService.UpdateAsync(tarefaItem, currentUser);
                }
                catch (DbUpdateConcurrencyException) //Tratamento de concorrência (2 usuários editando ao mesmo tempo)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(tarefaItem);
        }
    }
}