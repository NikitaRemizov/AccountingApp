﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DAO.Models
{
    public abstract class BudgetModel : Model
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
