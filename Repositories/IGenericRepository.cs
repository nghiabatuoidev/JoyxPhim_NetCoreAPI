using Backend.Models;
using Backend.ViewModels;
using System.Linq.Expressions;

namespace Backend.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int page=1, int pageSize=10);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
