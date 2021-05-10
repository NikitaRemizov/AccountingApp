using DAO.Models;
using DAO.Repositories.Interfaces;

namespace DAO.Repositories
{
    // TODO: all operations must include BudgetType information
    public class BudgetChangeRepository : BudgetRepository<BudgetChange>
    {
        public BudgetChangeRepository(IAccountingUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
