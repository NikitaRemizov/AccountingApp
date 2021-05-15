using AccountingApp.Models;
using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    [Route("budget/change")]
    public class BudgetChangeController : BudgetController<BudgetChangeDTO, BudgetChange>
    {
        public override IBudgetChangeService<BudgetChangeDTO> Service { get; }

        public BudgetChangeController(IBudgetChangeService<BudgetChangeDTO> service, IMapper mapper)
            : base(mapper)
        {
            Service = service;
        }

        [HttpGet("fordate")]
        public async Task<IActionResult> Get([FromQuery]DateTime date)
        {
            return Ok(Mapper.Map<IEnumerable<BudgetChange>>(
                await Service.GetForDate(date)));
        }

        [HttpGet("betweendates")]
        public async Task<IActionResult> Get([FromQuery]DateTime from, [FromQuery]DateTime to)
        {
            return Ok(Mapper.Map<IEnumerable<BudgetChange>>(
                await Service.GetBetweenDates(from, to)));
        }

        public override NotFoundObjectResult NotFound()
        {
            return NotFound(WrapError($"The incorrect {nameof(BudgetChange)} Id or {nameof(BudgetType)}Id"));
        }
    }
}
