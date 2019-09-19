using PERFECT.CLICK.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERFECT.CLICK.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RegistrationContext _db;
        public UnitOfWork(RegistrationContext context)
        {
            _db = context;
        }

        public void Save()
        {

            _db.SaveChanges();

        }
        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
