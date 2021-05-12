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
    // TODO: all operations must include BudgetType information
    // TODO: EVERYWHERE add special values instead of nulls
    public class BudgetChangeRepository : BudgetRepository<BudgetChange>
    {
        public BudgetChangeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<BudgetChange>> GetAll()
        {
            return await Set
                .Include(m => m.BudgetType)
                .Where(m => m.UserId == UserId)
                .SelectRequiredColumns()
                .ToListAsync();
        }

        public override async Task<BudgetChange> Get(Guid id)
        {
            return await Set
                .Include(m => m.BudgetType)
                .Where(m => m.UserId == UserId)
                .Where(m => m.Id == id)
                .SelectRequiredColumns()
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<BudgetChange>> Find(Expression<Func<BudgetChange, bool>> predicate)
        {
            return await Set
                .Include(m => m.BudgetType)
                .Where(m => m.UserId == UserId)
                .Where(predicate)
                .SelectRequiredColumns()
                .ToListAsync();
        }
    }
}
