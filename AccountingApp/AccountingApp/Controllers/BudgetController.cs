using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AccountingApp.Shared.Models;
using AccountingApp.Shared.Models.Validation;

namespace AccountingApp.Controllers
{
    [Authorize]
    public abstract class BudgetController<TDto, TModel> : AccountingController where TDto : BudgetDTO where TModel : BudgetModel
    {
        public class IdResponse 
        {
            public Guid Id { get; set; }
        }

        public virtual IBudgetService<TDto> Service { get; }
        protected IMapper Mapper { get; }

        public BudgetController(IBudgetService<TDto> service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        protected BudgetController(IMapper mapper)
        {
            Mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public virtual async Task<IActionResult> Create(TModel budgetModel)
        {
            if (budgetModel is null)
            {
                return InvalidObject();
            }
            var createdItemId = await Service.Create(Mapper.Map<TDto>(budgetModel));
            if (createdItemId == Guid.Empty)
            {
                return NotFound();
            }
            return Ok(new IdResponse{ Id = createdItemId });
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var deletedItemId = await Service.Delete(id);
            if (deletedItemId == Guid.Empty)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        [ValidateModel]
        public virtual async Task<IActionResult> Update(TModel budgetModel)
        {
            if (budgetModel is null)
            {
                return InvalidObject();
            }
            var updatedItemId = await Service.Update(Mapper.Map<TDto>(budgetModel));
            if (updatedItemId == Guid.Empty)
            {
                return NotFound();
            }
            return Ok();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (!await TryInitializeUser())
            {
                context.Result = StatusCode(500, "Unable to find authentificated user");
                return;
            }

            await next();
        }

        new public virtual NotFoundObjectResult NotFound()
        {
            return NotFound(WrapError("The provided id does not exist"));
        }

        protected async Task<bool> TryInitializeUser()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            try
            {
                await Service.SetUser(userEmail);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

    }
}
