using System;

namespace AccountingApp.BLL.DTO
{
    public class BudgetChangeDTO : BudgetDTO
    {
        public DateTime Date { get; set; }
        /// <summary> Amount of income (positive) or expense (negative) in cents</summary>
        public long Amount { get; set; }
        public Guid BudgetTypeId { get; set; }
        public string BudgetTypeName { get; set; }
    }
}
