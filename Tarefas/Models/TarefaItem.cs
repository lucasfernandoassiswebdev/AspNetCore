using System;

namespace Tarefas.Models
{
    public class TarefaItem
    {
        public Guid Id {get;set;}
        public bool EstaCompleta { get; set; } 
        public string Nome { get; set; }
        public DateTimeOffset? DataConclusao { get; set; }
    }
}