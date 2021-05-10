using System;

namespace BLL.DTO
{
    public abstract class BudgetDTO
    {
        public Guid Id { get; set; }
        // TODO: consider deleting this
        public Guid UserId { get; set; }
    }
}
