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
    public class BudgetChangeService : BudgetService<BudgetChangeDTO, BudgetChange>, IBudgetChangeService<BudgetChangeDTO>
    {
        public Guid? UserId { get; set; }
        private static readonly IEnumerable<BudgetChangeDTO> EmptyBugdetChanges = new List<BudgetChangeDTO>();

        public BudgetChangeService(IBudgetRepository<BudgetChange> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public virtual async Task<IEnumerable<BudgetChangeDTO>> GetBetweenDates(DateTime from, DateTime to)
        {
            if (to < from)
            {
                return EmptyBugdetChanges;
            }
            return _mapper.Map<IEnumerable<BudgetChangeDTO>>(
                await _repository.Find(m => m.Date.Date >= from.Date && m.Date.Date <= to.Date));
        }

        public virtual async Task<IEnumerable<BudgetChangeDTO>> GetForDate(DateTime date)
        {
            return _mapper.Map<IEnumerable<BudgetChangeDTO>>(
                await _repository.Find(m => m.Date.Date == date.Date));
        }
    }
}
