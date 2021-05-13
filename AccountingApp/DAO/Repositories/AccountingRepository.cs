using AutoMapper;
using DAO.Models;
using DAO.Repositories.Interfaces;
using DAO.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAO.Repositories
{
    public abstract class AccountingRepository<T> : IRepository<T> where T : Model
    {
        protected static Func<T, T, T> _map;
        protected DbContext _dbContext;
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

        public virtual async Task Update(T item)
        {
            var itemToUpdate = await Set.FindAsync(item.Id);
            if (itemToUpdate is null)
            {
                return;
            }
            _map(item, itemToUpdate);
        }

        public virtual async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public virtual async Task<T> Get(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync();
        }

        public virtual async Task Create(T user)
        {
            await Set.AddAsync(user);
        }

        public virtual async Task Delete(Guid id)
        {
            var itemToDelete = await Set.FindAsync(id);
            if (itemToDelete is null)
            {
                return;
            }
            Set.Remove(itemToDelete);
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
