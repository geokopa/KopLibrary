using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KopLibrary.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Method Declarations
        int SaveChanges();
        Task<int> SaveChangesAsync();
        #endregion
    }
}
