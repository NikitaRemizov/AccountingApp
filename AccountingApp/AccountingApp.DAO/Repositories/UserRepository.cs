using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;

namespace AccountingApp.DAO.Repositories
{
    public class UserRepository : AccountingRepository<User>
    {
        public UserRepository(IAccountingUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
