using Sistema_Web_Gestao.InterfaceRepositorio;
using Sistema_Web_Gestao.Models;
using System.Linq.Expressions;

namespace Sistema_Web_Gestao.Services
{
    public class TaferaService : ITaferaService
    {

        private readonly IRepository<Tarefa> _repository;

        public TaferaService(IRepository<Tarefa> repository)
        {
            _repository = repository;
        }



        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            await _repository.AddAsync(tarefa);
        }


        public async Task DeleteTarefaAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            return await _repository.GetAllAsync();
        }


        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            await _repository.UpdateAsync(tarefa);
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasWithIncludesAsync(params Expression<Func<Tarefa, object>>[] includes)
        {
            return await _repository.GetAllWithIncludesAsync(includes);
        } 
    }
}
