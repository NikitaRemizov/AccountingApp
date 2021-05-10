using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.Models
{
    [Table("budgetChange")]
    public class BudgetChange : BudgetModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public long Amount { get; set; }
        [Required]
        public BudgetType BudgetType { get; set; }
    }
}
