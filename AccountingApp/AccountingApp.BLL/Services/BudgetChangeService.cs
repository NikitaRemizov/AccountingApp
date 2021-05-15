using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.DAO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.BLL.Services
{
    public class BudgetChangeService : BudgetService<BudgetChangeDTO, BudgetChange>, IBudgetChangeService<BudgetChangeDTO>
    {
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

        public override async Task<Guid> Create(BudgetChangeDTO dto)
        {
            return await ExecuteAndCheckIfBudgetTypeIsCorrect(() => base.Create(dto));
        }

        public override async Task<Guid> Update(BudgetChangeDTO dto)
        {
            return await ExecuteAndCheckIfBudgetTypeIsCorrect(() => base.Update(dto));
        }
        private static async Task<Guid> ExecuteAndCheckIfBudgetTypeIsCorrect(Func<Task<Guid>> func)
        {
            if (func is null)
            {
                throw new ArgumentException(
                    "The function to execute is not provided", nameof(func));
            }

            try
            {
                return await func();
            }
            catch (InvalidEntityException)
            {
                return default;
            }
        }

    }
}
