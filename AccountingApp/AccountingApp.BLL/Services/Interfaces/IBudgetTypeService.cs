using AccountingApp.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.BLL.Services.Interfaces
{
    public interface IBudgetTypeService<T> : IBudgetService<T> where T : DTO.BudgetTypeDTO
    {
        Task<IEnumerable<DTO.BudgetTypeDTO>> GetAll();
    }
}
