using Autofac;
using AccountingApp.DAO.Databases;
using AccountingApp.DAO.Repositories;
using AccountingApp.DAO.Repositories.Interfaces;

namespace AccountingApp.DAO.Utils
{
    public class DAOModelsDependencyInjection : Module
    {
        private readonly string _connectionString;

        public DAOModelsDependencyInjection(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => 
                new AccountingUnitOfWork(
                    new AccountingDbContext(_connectionString)
                ))
                .As<IAccountingUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
