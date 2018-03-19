using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Services
{
    public class TempTarefaItemService : ITarefaItemService
    {
        public Task<bool> AdicionarItemAsync(TarefaItem novoItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarItem(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TarefaItem>> GetItemAsync(bool? criterio)
        {
            IEnumerable<TarefaItem> items = new []
            {
                new TarefaItem{
                    Nome = "Aprender ASP.NET Core 2.0",
                    EstaCompleta = false,
                    DataConclusao = DateTimeOffset.Now.AddDays(30)
                },
                new TarefaItem{
                    Nome = "Criar aplicações web",
                    EstaCompleta = false,
                    DataConclusao = DateTimeOffset.Now.AddDays(60)
                }
            };

            return Task.FromResult(items);
        }

        public TarefaItem GetTarefaById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TarefaItem item)
        {
            throw new NotImplementedException();
        }
    }
}