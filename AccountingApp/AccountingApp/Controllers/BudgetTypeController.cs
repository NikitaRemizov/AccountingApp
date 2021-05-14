using AccountingApp.Models;
using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    [Route("budgettype")]
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
            await InitializeUser();
            return Ok(Mapper.Map<IEnumerable<BudgetType>>(
                await Service.GetAll()));
        }
    }
}
