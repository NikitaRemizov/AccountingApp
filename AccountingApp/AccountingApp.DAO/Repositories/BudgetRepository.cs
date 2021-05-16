using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountingApp.DAO.Repositories
{
    public abstract class BudgetRepository<T> : AccountingRepository<T>, IBudgetRepository<T> where T : BudgetModel
    {
        private Guid? _userId;
        protected Guid UserId { 
            get
            {
                return _userId ?? throw new NullReferenceException(
                    $"'{nameof(UserId)}' is set to null. " +
                    $"Before invoking any other methods set current user using '{nameof(SetUser)}' method");
            }
        }


        protected override IQueryable<T> SetAsQueryable => base.SetAsQueryable.Where(m => m.UserId == UserId);

        public BudgetRepository(IAccountingUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task SetUser(string email)
        {
            var userId = await _dbContext.Set<User>()
                .Where(u => u.Email == email)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (userId == Guid.Empty)
            {
                _userId = null;
                throw new ArgumentException(
                    $"The provided email doesn't belong to any user", nameof(email));
            }

            _userId = userId;
        }

        public override async Task<T> Create(T item)
        {
            item.UserId = UserId;
            return await base.Create(item);
        }
    }
}
