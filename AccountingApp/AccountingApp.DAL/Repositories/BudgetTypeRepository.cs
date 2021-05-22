using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using AccountingApp.DAL.Utils;
using System.Linq;

namespace AccountingApp.DAL.Repositories
{
    public class BudgetTypeRepository : BudgetRepository<BudgetType>
    {
        protected override IQueryable<BudgetType> SetAsQueryable => base.SetAsQueryable
                                                                    .SelectRequiredColumns();
        public BudgetTypeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
