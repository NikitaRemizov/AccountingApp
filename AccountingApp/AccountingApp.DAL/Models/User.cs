using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingApp.DAL.Models
{
    [Table("users")]
    public class User : Model
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public List<BudgetType> BudgetTypes { get; set; }
        public List<BudgetChange> BudgetChanges { get; set; }
    }
}
