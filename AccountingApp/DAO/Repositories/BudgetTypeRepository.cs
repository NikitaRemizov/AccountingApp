using DAO.Models;
using DAO.Repositories.Interfaces;
using DAO.Utils;
using System.Linq;

namespace DAO.Repositories
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
