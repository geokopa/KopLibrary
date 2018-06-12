using KopLibrary.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KopLibrary.Repository
{
    public class RepositoryBase<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> Set;

        public RepositoryBase(DbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        #region Async Methods
        public virtual async Task<T> GetAsync(int? id)
        {
            return await Set.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync()
        {
            return await Set.AsNoTracking().SingleOrDefaultAsync();
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            int result;
            if (predicate != null)
            {
                result = await Set.AsNoTracking().CountAsync(predicate);
            }
            else
            {
                result = await Set.AsNoTracking().CountAsync();
            }

            return result;
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string queryText, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(new RawSqlString(queryText), parameters);
        }

        #endregion

        #region Sync Methods

        public virtual T Get(int? id)
        {
            return Set.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Set.AsNoTracking();
        }

        public virtual IQueryable<T> QueryAll()
        {
            return Set.AsNoTracking().AsQueryable();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Set.Where(predicate).AsNoTracking();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return Set.AsNoTracking().FirstOrDefault();
            }
            return Set.AsNoTracking().FirstOrDefault(predicate);
        }

        public virtual void Add(T entity)
        {
            Set.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Set.AddRange(entities);
        }

        public virtual void Remove(int key)
        {
            var entity = Get(key);
            Remove(entity);
        }

        public virtual void Remove(T entity)
        {
            if (!Set.Local.Contains(entity))
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }

        public virtual void SetModifyState(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Update(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = Context.Set<T>().Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = await Context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                var entry = Context.Entry(existing);
                entry.CurrentValues.SetValues(updated);
                await Context.SaveChangesAsync();
            }
            else
            {
                SetModifyState(updated);
            }
            return existing;
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking().Any(predicate);
        }

        public virtual IQueryable<T> IncludeMultiple(params Expression<Func<T, object>>[] includes)
        {
            var query = QueryAll();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return query;
        }

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            return Set.AsNoTracking().Count(expression);
        }

        #endregion
    }
}
