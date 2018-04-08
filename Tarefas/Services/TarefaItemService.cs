using System;
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
                OwnerId = novoItem.OwnerId,
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

        public async Task<IEnumerable<TarefaItem>> GetItemAsync(bool? criterio, ApplicationUser user)
        {
            if (criterio != null)
                return await _context.Tarefas
                                .Where(t => t.EstaCompleta == criterio && t.OwnerId == null || t.OwnerId == user.Id)
                                .ToArrayAsync();

            return await _context.Tarefas
                            .Where(t => t.OwnerId == null || t.OwnerId == user.Id)
                            .ToArrayAsync();
        }

        public TarefaItem GetTarefaById(int? id) => _context.Tarefas.Find(id);

        public async Task UpdateAsync(TarefaItem item, ApplicationUser user)
        {
            if(item.OwnerId == null)
                item.OwnerId = user.Id;

            if (item == null)
                throw new ArgumentException(nameof(item));

            _context.Tarefas.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}