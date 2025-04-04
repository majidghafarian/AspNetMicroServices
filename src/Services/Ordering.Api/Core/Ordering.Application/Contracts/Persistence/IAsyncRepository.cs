using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> peredicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> peredicate = null,
                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                   string includestring = null,
                   bool disabletracking = true);


        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> peredicate = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                        List<Expression<Func<T, object>>> includes = null,
                        bool disabletracking = true);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
