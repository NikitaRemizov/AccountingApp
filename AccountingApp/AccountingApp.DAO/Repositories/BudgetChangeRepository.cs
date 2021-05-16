using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.DAO.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.DAO.Repositories
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
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new InvalidEntityException(
                    $"Can not create BudgetChange entity with non existing BudgetType. Specify correct BudgetType");
            }
        }
    }
}
