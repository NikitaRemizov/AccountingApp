using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBudgetTypeService<T> : IBudgetService<T> where T : BudgetTypeDTO
    {
        Task<IEnumerable<BudgetTypeDTO>> GetAll();
    }
}
