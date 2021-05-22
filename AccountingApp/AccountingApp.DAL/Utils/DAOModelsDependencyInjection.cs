using Autofac;
using AccountingApp.DAL.Databases;
using AccountingApp.DAL.Repositories;
using AccountingApp.DAL.Repositories.Interfaces;

namespace AccountingApp.DAL.Utils
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
