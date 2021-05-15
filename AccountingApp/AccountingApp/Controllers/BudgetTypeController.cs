using AccountingApp.Models;
using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    [Route("budget/type")]
    public class BudgetTypeController : BudgetController<BudgetTypeDTO, BudgetType>
    {
        public override IBudgetTypeService<BudgetTypeDTO> Service { get; }

        public BudgetTypeController(IBudgetTypeService<BudgetTypeDTO> service, IMapper mapper)
            : base(mapper)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(Mapper.Map<IEnumerable<BudgetType>>(
                await Service.GetAll()));
        }
    }
}
