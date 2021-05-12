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
    public abstract class BudgetRepository<T> : AccountingRepository<T>, IBudgetRepository<T> where T : BudgetModel
    {
        // TODO: change parent methods to check if the user data belongs to the user
        private Guid? _userId;
        protected Guid UserId => _userId ?? throw new NullReferenceException(
            $"'{nameof(UserId)}' is set to null. " +
            $"Before invoking any other methods set '{nameof(UserId)}' using '{nameof(SetUser)}' method");

        protected virtual IQueryable<T> TableOfUser => Set.Where(m => m.UserId == UserId);

        public BudgetRepository(IAccountingUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task SetUser(string email)
        {
            var userId = await _dbContext.Set<User>()
                .Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (userId == Guid.Empty)
            {
                // TODO: change exception type
                throw new Exception();
            }

            _userId = userId;
        }

        public override async Task<T> Get(Guid id)
        {
            return await TableOfUser
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<T>> GetAll()
        {
            return await TableOfUser.ToListAsync();
        }

        public override async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await TableOfUser
                .Where(predicate)
                .ToListAsync();
        }

        public override Task Create(T user)
        {
            user.UserId = UserId;
            return base.Create(user);
        }

        protected override async Task<T> FindAsync(Guid id)
        {
            return await TableOfUser
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
