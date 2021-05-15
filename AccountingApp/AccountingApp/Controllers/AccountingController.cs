using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.Controllers
{
    public abstract class AccountingController : Controller
    {
        protected virtual object WrapError(string message)
        {
            return new { error = message };
        }

        protected virtual BadRequestObjectResult InvalidObject()
        {
            return BadRequest(WrapError("Invalid object provided"));
        }
    }
}
