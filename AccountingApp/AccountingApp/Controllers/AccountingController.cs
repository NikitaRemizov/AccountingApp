using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.Controllers
{
    public abstract class AccountingController : Controller
    {
        protected virtual object WrapError(string message)
        {
            return new { error = message };
        }
    }
}
