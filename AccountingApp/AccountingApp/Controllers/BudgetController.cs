using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    // TODO: check all possible combiantion of wrong fields in provided models
    [Authorize]
    public abstract class BudgetController<T> : Controller where T : BudgetDTO
    {
        public virtual IBudgetService<T> Service { get; }

        public BudgetController(IBudgetService<T> service)
        {
            Service = service;
        }

        protected BudgetController()
        {
        }

        // TODO: solve the problem with datetime format in json (probably have to crete PL model and use model attributes)
        [HttpPost]
        public virtual async Task<IActionResult> Create(T budgetDto)
        {
            // TODO: properly catch exceptions
            try
            {
                await InitializeUser();
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                await Service.Create(budgetDto);
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
            await InitializeUser();
            await Service.Delete(id);
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(T budgetDto)
        {
            await InitializeUser();
            await Service.Update(budgetDto);
            return Ok();
        }

        protected async Task InitializeUser()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            await Service.SetUser(userEmail);
        }
    }
}
