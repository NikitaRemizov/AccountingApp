using AccountingApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AccountingApp.Tests")]
namespace AccountingApp.DAL.Repositories
{
    internal class AccountingUnitOfWork : IAccountingUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _isDisposed = false;

        private Dictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();

        public AccountingUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        void IAccountingUnitOfWork.Register(IRepository repository)
        {
            var type = repository.GetType();
            if (_repositories.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"The provided {nameof(IRepository)}<{type.Name}> is already registered");
            }
            repository.SetDbContext(_dbContext);
            _repositories.Add(repository.GetType(), repository);
        }
    }
}
