using DAO.Models;
using DAO.Repositories.Interfaces;
using DAO.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAO.Repositories
{
    // TODO: EVERYWHERE add special values instead of nulls
    // TODO: handle case when budgetTypeId is incorrect and think about other such cases
    public class BudgetChangeRepository : BudgetRepository<BudgetChange>
    {
        protected override IQueryable<BudgetChange> TableOfUser => Set
                                                                   .Include(m => m.BudgetType)
                                                                   .Where(m => m.UserId == UserId)
                                                                   .SelectRequiredColumns();

        public BudgetChangeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
