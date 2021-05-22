using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using AccountingApp.DAL.Utils;

namespace AccountingApp.BLL.Services
{
    public class BudgetService<TDto, TModel> : IBudgetService<TDto> where TDto : BudgetDTO where TModel : BudgetModel
    {
        protected readonly IBudgetRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        public BudgetService(IBudgetRepository<TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task SetUser(string email)
        {
            await _repository.SetUser(email);
        }

        public virtual async Task<Guid> Create(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);
            var createdModel = await _repository.Create(model);
            if (createdModel is null)
            {
                return Guid.Empty;
            }
            await _repository.Save();
            return createdModel.Id;
        }

        public virtual async Task<Guid> Delete(Guid id)
        {
            var deletedItemId = await _repository.Delete(id);
            await _repository.Save();
            return deletedItemId;
        }

        public virtual async Task<Guid> Update(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);
            var updatedItemId = await _repository.Update(model);
            await _repository.Save();
            return updatedItemId;
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
