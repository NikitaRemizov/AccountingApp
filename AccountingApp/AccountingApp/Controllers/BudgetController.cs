using AccountingApp.Models;
using AccountingApp.Models.Validation;
using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    // TODO: check all possible combiantion of wrong fields in provided models
    [Authorize]
    public abstract class BudgetController<TDto, TModel> : Controller where TDto : BudgetDTO where TModel : BudgetModel
    {
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

        // TODO: solve the problem with datetime format in json (probably have to crete PL model and use model attributes)
        [HttpPost]
        [ValidateModel]
        public virtual async Task<IActionResult> Create(TModel budgetModel)
        {
            // TODO: properly catch exceptions

            try
            {
                await Service.Create(Mapper.Map<TDto>(budgetModel));
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await Service.Delete(id);
            return Ok();
        }

        [HttpPut]
        [ValidateModel]
        public virtual async Task<IActionResult> Update(TModel budgetModel)
        {
            await Service.Update(Mapper.Map<TDto>(budgetModel));
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
                context.Result = StatusCode(500);
                return;
            }

            await next();
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
