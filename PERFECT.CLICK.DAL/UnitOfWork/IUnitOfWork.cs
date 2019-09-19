using System;
using System.Collections.Generic;
using System.Text;

namespace PERFECT.CLICK.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}
