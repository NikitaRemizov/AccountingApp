using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.BLL.Services
{
    public class BudgetTypeService : BudgetService<BudgetTypeDTO, BudgetType>, IBudgetTypeService<BudgetTypeDTO>
    {
        public BudgetTypeService(IBudgetRepository<BudgetType> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public virtual async Task<IEnumerable<BudgetTypeDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<BudgetTypeDTO>>(
                await _repository.GetAll());
        }
    }
}
