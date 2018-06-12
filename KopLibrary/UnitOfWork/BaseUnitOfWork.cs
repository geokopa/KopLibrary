using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KopLibrary.UnitOfWork
{
    public abstract class BaseUnitOfWork
    {
        readonly DbContext _context;

        protected BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }

        #region Public Methods


        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region IDisposable Members

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
