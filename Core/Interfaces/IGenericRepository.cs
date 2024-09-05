
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
     Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
      Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
   
    }
}