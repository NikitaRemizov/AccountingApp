using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.Tests.Repositories.Utils;
using System.Collections.Generic;

namespace AccountingApp.Tests.Repositories
{
    public class BudgetTypeRepositoryTests : BudgetRepositoryTests<BudgetType>
    {
        protected override IEnumerable<BudgetType> TestModel { get; }
        protected override IBudgetRepository<BudgetType> Repository { get; }

        public BudgetTypeRepositoryTests()
        {
            TestModel = TestData.BudgetTypes;
            Repository = new BudgetTypeRepository(UnitOfWork);
            Repository.SetUser(TestData.User.Email).Wait();
        }
    }
}
