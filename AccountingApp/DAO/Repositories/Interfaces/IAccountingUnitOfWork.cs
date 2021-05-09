using System;

namespace DAO.Repositories.Interfaces
{
    public interface IAccountingUnitOfWork : IDisposable
    {
        internal void Register(IRepository repository);
    }
}
