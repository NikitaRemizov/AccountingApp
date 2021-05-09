using DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAO.Repositories.Interfaces
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
        Task Create(T user);
        Task Update(T user);
        Task Delete(Guid id);
    }
}
