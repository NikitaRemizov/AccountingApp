using AutoMapper;
using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using AccountingApp.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountingApp.DAL.Repositories
{
    public abstract class AccountingRepository<T> : IRepository<T> where T : Model
    {
        protected static Func<T, T, T> _map;
        protected DbContext _dbContext;
        protected virtual IQueryable<T> SetAsQueryable => _dbContext.Set<T>().AsNoTracking();
        protected virtual DbSet<T> Set => _dbContext.Set<T>();
        private Action _dispose;

        static AccountingRepository()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAOModelsMapperProfile>();
            });
            _map = mapperConfiguration.CreateMapper().Map;
        }

        public AccountingRepository(IAccountingUnitOfWork unitOfWork)
        {
            unitOfWork.Register(this);
            _dispose = unitOfWork.Dispose;
        }

        public virtual async Task<Guid> Update(T item)
        {
            var itemToUpdate = await Get(item.Id);
            if (itemToUpdate is null)
            {
                return Guid.Empty;
            }
            _map(item, itemToUpdate);
            return itemToUpdate.Id;
        }

        public virtual async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await SetAsQueryable.ToListAsync();
        }

        public virtual async Task<T> Get(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await SetAsQueryable.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> Create(T item)
        {
            return (await Set.AddAsync(item))?.Entity;
        }

        public virtual async Task<Guid> Delete(Guid id)
        {
            var itemToDelete = await Get(id);
            if (itemToDelete is null)
            {
                return Guid.Empty;
            }
            Set.Remove(itemToDelete);
            return itemToDelete.Id;
        }

        public void Dispose()
        {
            _dispose?.Invoke();
            GC.SuppressFinalize(this);
        }

        void IRepository.SetDbContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
