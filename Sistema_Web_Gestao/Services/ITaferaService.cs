using Sistema_Web_Gestao.Models;
using System.Linq.Expressions;

namespace Sistema_Web_Gestao.Services
{

    // Interface de Tarefa
    public interface ITaferaService
    {
        Task<IEnumerable<Tarefa>> GetAllTarefasAsync();
        Task<Tarefa> GetTarefaByIdAsync(int id);
        Task AddTarefaAsync(Tarefa tarefa);
        Task UpdateTarefaAsync(Tarefa tarefa);
        Task DeleteTarefaAsync(int id);
        Task<IEnumerable<Tarefa>> GetAllTarefasWithIncludesAsync(params Expression<Func<Tarefa, object>>[] includes);
    }
}
