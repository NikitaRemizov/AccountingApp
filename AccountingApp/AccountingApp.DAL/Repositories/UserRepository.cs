using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;

namespace AccountingApp.DAL.Repositories
{
    public class UserRepository : AccountingRepository<User>
    {
        public UserRepository(IAccountingUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
