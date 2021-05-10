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

        public virtual async Task SetUser(Guid id)
        {
            await _repository.SetUser(id);
        }

        public virtual async Task Create(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);
            await _repository.Create(model);
            await _repository.Save();
        }

        public virtual async Task Delete(Guid id)
        {
            var models = await _repository
                .Find(m => m.Id == id);

            var model = models.FirstOrDefault();
            if (model is null)
            {
                return;
            }

            await _repository.Delete(id);
            await _repository.Save();
        }

        public virtual async Task Update(TDto dto)
        {
            var models = await _repository.Find(m => m.Id == dto.Id);
            var model = models.FirstOrDefault();
            if (model is null)
            {
                return;
            }

            _mapper.Map(dto, model);
            await _repository.Update(model);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
