using System;

namespace AccountingApp.DAO.Repositories.Interfaces
{
    public interface IAccountingUnitOfWork : IDisposable
    {
        internal void Register(IRepository repository);
    }
}
