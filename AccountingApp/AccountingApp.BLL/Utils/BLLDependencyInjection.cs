using Autofac;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories;
using AccountingApp.DAO.Repositories.Interfaces;

namespace AccountingApp.BLL.Utils
{
    public class BLLDependencyInjection : Module
    {
        private readonly string _connectionString;

        public BLLDependencyInjection(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterModule(
                    new AccountingApp.DAO.Utils.DAOModelsDependencyInjection(_connectionString)
                );
            builder
                .RegisterType<UserRepository>()
                .As<IRepository<User>>();
            builder
                .RegisterType<BudgetChangeRepository>()
                .As<IBudgetRepository<BudgetChange>>();
            builder
                .RegisterType<BudgetTypeRepository>()
                .As<IBudgetRepository<BudgetType>>();
        }
    }
}
