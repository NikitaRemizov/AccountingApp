using AccountingApp.DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountingApp.DAO.Repositories.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task Save();
        internal void SetDbContext(DbContext dbContext);
    }

    public interface IRepository<T> : IRepository where T : Model
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> Get(Guid id);
        Task<T> Create(T user);
        Task<Guid> Update(T user);
        Task<Guid> Delete(Guid id);
    }
}
