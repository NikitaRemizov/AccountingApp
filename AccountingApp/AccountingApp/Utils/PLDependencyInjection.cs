using Autofac;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services;
using AccountingApp.BLL.Services.Interfaces;

namespace AccountingApp.Utils
{
    public class PLDependencyInjection : Autofac.Module
    {
        public string _connectionString;

        public PLDependencyInjection(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AccountingApp.BLL.Utils.BLLDependencyInjection(_connectionString));
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<BudgetTypeService>().As<IBudgetTypeService<BudgetTypeDTO>>();
            builder.RegisterType<BudgetChangeService>().As<IBudgetChangeService<BudgetChangeDTO>>();
        }
    }
}
