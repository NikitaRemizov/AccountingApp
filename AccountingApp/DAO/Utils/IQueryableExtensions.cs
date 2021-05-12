using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Utils
{
    internal static class IQueryableExtensions
    {
        public static IQueryable<BudgetChange> SelectRequiredColumns(this IQueryable<BudgetChange> records)
        {
            return records.Select(bc => new BudgetChange
            {
                Id = bc.Id,
                Amount = bc.Amount,
                Date = bc.Date,
                UserId = bc.UserId,
                User = bc.User,
                BudgetType = TakeBudgetType(bc)
            });
        }

        private static BudgetType TakeBudgetType(BudgetChange budgetChange)
        {
            return new BudgetType
            {
                Id = budgetChange.BudgetType.Id,
                Name = budgetChange.BudgetType.Name
            };
        }
    }
}
