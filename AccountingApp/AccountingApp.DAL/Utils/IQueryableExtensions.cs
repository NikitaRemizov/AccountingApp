using AccountingApp.DAL.Models;
using System.Linq;

namespace AccountingApp.DAL.Utils
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
                BudgetTypeId = bc.BudgetType.Id,
                BudgetType = new BudgetType { Name = bc.BudgetType.Name}
            });
        }

        public static IQueryable<BudgetType> SelectRequiredColumns(this IQueryable<BudgetType> records)
        {
            return records.Select(bt => new BudgetType
            {
                Id = bt.Id,
                Name = bt.Name
            });
        }
    }
} 