using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    [Route("budgettype")]
    public class BudgetTypeController : BudgetController<BudgetTypeDTO>
    {
        public override IBudgetTypeService<BudgetTypeDTO> Service { get; }

        public BudgetTypeController(IBudgetTypeService<BudgetTypeDTO> service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await InitializeUser();
            return Ok(await Service.GetAll());
        }
    }
}
