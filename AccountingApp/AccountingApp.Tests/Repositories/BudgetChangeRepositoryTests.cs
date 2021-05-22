using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories;
using AccountingApp.DAL.Repositories.Interfaces;
using AccountingApp.DAL.Utils;
using AccountingApp.Tests.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AccountingApp.Tests.Repositories
{
    public class BudgetChangeRepositoryTests : BudgetRepositoryTests<BudgetChange>
    {
        protected override IEnumerable<BudgetChange> TestModel { get; }
        protected override IBudgetRepository<BudgetChange> Repository { get; }

        public BudgetChangeRepositoryTests()
        {
            TestModel = TestData.BudgetChanges;
            Repository = new BudgetChangeRepository(UnitOfWork);
            Repository.SetUser(TestData.User.Email).Wait();
        }

        [Fact]
        public void Save_CreateEntityWithInvalidBudgetTypeId_ThrowsInvalidEntityException()
        {
            var budgetChange = new BudgetChange
            {
                BudgetTypeId = Guid.NewGuid(),
                Id = TestData.BudgetChanges.First().Id,
            };

            Repository.Create(budgetChange).Wait();

            Assert.ThrowsAsync<InvalidEntityException>(
                () => Repository.Save());
        }

        [Fact]
        public void Save_UpdateEntityWithInvalidBudgetTypeId_ThrowsInvalidEntityException()
        {
            var budgetChange = Repository.Get(TestData.BudgetChanges.First().Id).Result;

            Repository.Create(budgetChange).Wait();

            Assert.ThrowsAsync<InvalidEntityException>(
                () => Repository.Save());
        }
    }
}
