using DAO.Models;
using DAO.Repositories.Interfaces;

namespace DAO.Repositories
{
    public class BudgetTypeRepository : BudgetRepository<BudgetType>
    {
        public BudgetTypeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
