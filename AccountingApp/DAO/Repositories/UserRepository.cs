using DAO.Models;
using DAO.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAO.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContext _dbContext;
        private Action _dispose;
        private DbSet<User> Set => _dbContext.Set<User>();
        public UserRepository(IAccountingUnitOfWork unitOfWork)
        {
            unitOfWork.Register(this);
            _dispose = unitOfWork.Dispose;
        }

        public async Task Update(User user)
        {
            var itemToUpdate = await Set.FindAsync(user.Id);
            if (itemToUpdate is null)
            {
                return;
            }
            itemToUpdate.Email = user.Email;
            itemToUpdate.Password = user.Password;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public async Task<User> Get(Guid id)
        {
            var user = await Set.FindAsync(id);
            return user ?? User.Empty;
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync();
        }

        public async Task Create(User user)
        {
            await Set.AddAsync(user);
        }

        public async Task Delete(Guid id)
        {
            var itemToDelete = await Set.FindAsync(id);
            if (itemToDelete is null)
            {
                return;
            }
            Set.Remove(itemToDelete);
        }

        void IRepository.SetDbContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dispose?.Invoke();
            GC.SuppressFinalize(this);
        }
    }
}
