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

        public async Task<IEnumerable<TarefaItem>> GetItemAsync() => await _context.Tarefas.Where(t => t.EstaCompleta == false).ToArrayAsync();
    }
}