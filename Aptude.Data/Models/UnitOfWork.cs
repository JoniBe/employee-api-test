using Aptude.Core.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;


namespace Aptude.Data.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // Track whether Dispose has been called.
        private bool _Disposed = false;

        private readonly AptudeDbContext _DataContext;

        public UnitOfWork(AptudeDbContext dataContext)
        {
            _DataContext = dataContext;
        }

        public IDbContextTransaction CreateTransaction()
        {
            return this._DataContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return _DataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing && _DataContext != null)
                {
                    _DataContext.Dispose();
                }

                _Disposed = true;
            }
        }
    }
}
