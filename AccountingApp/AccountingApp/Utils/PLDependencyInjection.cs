using Autofac;
using BLL.DTO;
using BLL.Services;
using BLL.Services.Interfaces;

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
            builder.RegisterModule(new BLL.Utils.BLLDependencyInjection(_connectionString));
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<BudgetTypeService>().As<IBudgetTypeService<BudgetTypeDTO>>();
            builder.RegisterType<BudgetChangeService>().As<IBudgetChangeService<BudgetChangeDTO>>();
        }
    }
}
