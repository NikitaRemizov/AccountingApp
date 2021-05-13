using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using DAO.Models;
using DAO.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
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
