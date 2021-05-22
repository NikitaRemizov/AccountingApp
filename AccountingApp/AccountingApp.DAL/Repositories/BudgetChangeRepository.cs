using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using AccountingApp.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.DAL.Repositories
{
    public class BudgetChangeRepository : BudgetRepository<BudgetChange>
    {
        protected override IQueryable<BudgetChange> SetAsQueryable => Set
                                                                      .Include(m => m.BudgetType)
                                                                      .Where(m => m.UserId == UserId)
                                                                      .SelectRequiredColumns();

        public BudgetChangeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task Save()
        {
            try
            {
                await base.Save();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidEntityException(
                    $"Can not create BudgetChange entity with non existing BudgetType. Specify correct BudgetType",
                    ex);
            }
        }
    }
}
