using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.Models
{
    [Table("budgetChange")]
    public class BudgetChange : BudgetModel
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        public long Amount { get; set; }
        public Guid? BudgetTypeId { get; set; }
        public BudgetType BudgetType { get; set; }
    }
}
