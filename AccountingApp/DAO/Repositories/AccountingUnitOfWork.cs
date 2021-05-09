using DAO.Models;
using DAO.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAO.Repositories
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

        public IRepository<T> Repository<T>() where T : Model
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"There is no {nameof(IRepository)}<{typeof(T).Name}> registered in unit-of-work");
            }
            return (IRepository<T>)_repositories[type];
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
