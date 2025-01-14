using System.Linq.Expressions;

namespace Sistema_Web_Gestao.InterfaceRepositorio
{
    public interface IRepository<T> where T : class
    {

        // Interface Repositorio da aplicação
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
    }
}
