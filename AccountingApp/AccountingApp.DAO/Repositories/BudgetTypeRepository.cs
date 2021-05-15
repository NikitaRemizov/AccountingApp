using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.DAO.Utils;
using System.Linq;

namespace AccountingApp.DAO.Repositories
{
    public class BudgetTypeRepository : BudgetRepository<BudgetType>
    {
        protected override IQueryable<BudgetType> TableOfUser => Set
                                                                 .Where(m => m.UserId == UserId)
                                                                 .SelectRequiredColumns();
        public BudgetTypeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
