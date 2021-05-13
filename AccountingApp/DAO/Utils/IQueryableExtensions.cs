using DAO.Models;
using System;
using System.Linq;

namespace DAO.Utils
{
    internal static class IQueryableExtensions
    {
        // TODO: Check if the query contains only required columns
        public static IQueryable<BudgetChange> SelectRequiredColumns(this IQueryable<BudgetChange> records)
        {
            return records.Select(bc => new BudgetChange
            {
                Id = bc.Id,
                Amount = bc.Amount,
                Date = bc.Date,
                BudgetType = TakeBudgetTypeWithName(bc),
                BudgetTypeId= TakeBudgetTypeId(bc)
            });
        }

        // TODO: Check if the query contains only required columns
        public static IQueryable<BudgetType> SelectRequiredColumns(this IQueryable<BudgetType> records)
        {
            return records.Select(bt => new BudgetType
            {
                Id = bt.Id,
                Name = bt.Name
            });
        }

        private static BudgetType TakeBudgetTypeWithName(BudgetChange budgetChange)
        {
            return new BudgetType
            {
                Name = budgetChange?.BudgetType?.Name
            };
        }

        private static Guid TakeBudgetTypeId(BudgetChange budgetChange)
        {
            return budgetChange?.BudgetType?.Id ?? Guid.Empty;
        }
    }
} 