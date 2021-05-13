using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using DAO.Models;
using DAO.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
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
            // consider validating email (or do this in DAO)
            await _repository.SetUser(email);
        }

        public virtual async Task Create(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);
            await _repository.Create(model);
            await _repository.Save();
        }

        public virtual async Task Delete(Guid id)
        {
            await _repository.Delete(id);
            await _repository.Save();
        }

        public virtual async Task Update(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);
            await _repository.Update(model);
            await _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
