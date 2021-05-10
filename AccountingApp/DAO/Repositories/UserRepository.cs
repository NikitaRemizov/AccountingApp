﻿using DAO.Models;
using DAO.Repositories.Interfaces;

namespace DAO.Repositories
{
    public class UserRepository : AccountingRepository<User>
    {
        public UserRepository(IAccountingUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}