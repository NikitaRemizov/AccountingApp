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
