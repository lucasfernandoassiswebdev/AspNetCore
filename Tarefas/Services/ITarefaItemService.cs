using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Services
{
    public interface ITarefaItemService
    {
         Task<IEnumerable<TarefaItem>> GetItemAsync(bool? criterio);
         Task<bool> AdicionarItemAsync(TarefaItem novoItem);
         Task<bool> DeletarItem(int? id);
         TarefaItem GetTarefaById(int? id);
         Task UpdateAsync(TarefaItem item);
    }
}