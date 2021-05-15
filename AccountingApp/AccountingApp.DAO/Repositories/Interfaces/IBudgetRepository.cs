﻿using AccountingApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountingApp.DAO.Repositories.Interfaces
{
    public interface IBudgetRepository<T> : IRepository<T> where T : BudgetModel
    {
        Task SetUser(string email);
    }
}