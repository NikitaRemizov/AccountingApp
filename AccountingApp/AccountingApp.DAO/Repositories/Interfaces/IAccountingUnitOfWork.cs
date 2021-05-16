using System;
using System.Runtime.CompilerServices;

namespace AccountingApp.DAO.Repositories.Interfaces
{
    public interface IAccountingUnitOfWork : IDisposable
    {
        internal void Register(IRepository repository);
    }
}
