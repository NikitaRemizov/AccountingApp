using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingApp.DAL.Models
{
    [Table("budgetTypes")]
    public class BudgetType : BudgetModel
    {
        [Required]
        public string Name { get; set; }
        public List<BudgetChange> BudgetChanges { get; set; }
    }
}
