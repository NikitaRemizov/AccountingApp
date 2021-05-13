using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    [Route("budgetchange")]
    public class BudgetChangeController : BudgetController<BudgetChangeDTO>
    {
        public override IBudgetChangeService<BudgetChangeDTO> Service { get; }

        public BudgetChangeController(IBudgetChangeService<BudgetChangeDTO> service)
        {
            Service = service;
        }

        [HttpGet("fordate")]
        public async Task<IActionResult> Get([FromQuery]DateTime date)
        {
            await InitializeUser();
            return Ok(await Service.GetForDate(date));
        }

        [HttpGet("betweendates")]
        public async Task<IActionResult> Get([FromQuery]DateTime from, [FromQuery] DateTime to)
        {
            await InitializeUser();
            return Ok(await Service.GetBetweenDates(from, to));
        }
    }
}
