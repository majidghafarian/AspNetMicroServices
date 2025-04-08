using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private readonly OrderContext _Context;
        public RepositoryBase(OrderContext context)
        {
            _Context= context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<T> AddAsync(T entity)
        {
            _Context.Set<T>().Add(entity);
            await _Context.SaveChangesAsync();  // استفاده از SaveChangesAsync
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> peredicate)
        {
            return await _Context.Set<T>().Where(peredicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(
      Expression<Func<T, bool>> predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      string includeString = null,
      bool disableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         List<Expression<Func<T, object>>> includes = null,
         bool disableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                includes.ForEach(include => query = query.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
         _Context.Entry(entity).State= EntityState.Modified;
         await   _Context.SaveChangesAsync();
            
        }
    }
}
