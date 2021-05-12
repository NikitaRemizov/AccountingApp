using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBudgetChangeService<T> : IBudgetService<T> where T : BudgetChangeDTO
    {
        /// <param name="from">The inclusive lower bound date</param>
        /// <param name="to">The exclusive upper bound date</param>
        Task<IEnumerable<BudgetChangeDTO>> GetBetweenDates(DateTime from, DateTime to);
        Task<IEnumerable<BudgetChangeDTO>> GetForDate(DateTime date);
    }
}
