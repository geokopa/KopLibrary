using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KopLibrary.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Async Methods
        Task<TEntity> GetAsync(int? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync();
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> ExecuteSqlCommandAsync(string queryText, params object[] parameters);
        #endregion

        #region Sync Methods
        TEntity Get(int? id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> QueryAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(int key);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void SetModifyState(TEntity entity);

        TEntity Update(TEntity updated, int key);
        Task<TEntity> UpdateAsync(TEntity updated, int key);

        bool Any(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includes);
        int Count(Expression<Func<TEntity, bool>> expression);

        #endregion
    }
}
