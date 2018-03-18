using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;

namespace Tarefas.Services
{
    public class TarefaItemService : ITarefaItemService
    {
        private readonly ApplicationDbContext _context;

        public TarefaItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarItemAsync(TarefaItem novoItem)
        {
            var tarefa = new TarefaItem
            {
                EstaCompleta = false,
                Nome = novoItem.Nome,
                DataConclusao = novoItem.DataConclusao
            };

            _context.Tarefas.Add(tarefa);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletarItem(int? id)
        {
            //Save changes retorna o numero de linhas afetadas
            TarefaItem tarefa = _context.Tarefas.Find(id);
            _context.Tarefas.Remove(tarefa);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<TarefaItem>> GetItemAsync() => await _context.Tarefas.Where(t => t.EstaCompleta == false).ToArrayAsync();

        public TarefaItem GetTarefaById(int? id)
        {
            return _context.Tarefas.Find(id);
        }
    }
}