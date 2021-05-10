using System;

namespace BLL.DTO
{
    public class BudgetChangeDTO : BudgetDTO
    {
        public DateTime Date { get; set; }
        /// <summary> Amount of income (positive) or expense (negative) in cents</summary>
        public long Amount { get; set; }
        public BudgetTypeDTO BudgetType { get; set; }
        public bool IsIncome => Amount >= 0;
    }
}
