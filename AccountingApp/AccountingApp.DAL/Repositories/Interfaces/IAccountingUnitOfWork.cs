using System;
using System.Runtime.CompilerServices;

namespace AccountingApp.DAL.Repositories.Interfaces
{
    public interface IAccountingUnitOfWork : IDisposable
    {
        internal void Register(IRepository repository);
    }
}
