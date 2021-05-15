using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountingApp.Tests.Services
{
    public class BudgetTypeServiceTests : BudgetServiceTests<BudgetTypeDTO, BudgetType>
    {
        public override BudgetType FakeModel { get; }
        public override BudgetTypeDTO FakeDTO { get; }
        public override Mock<IBudgetRepository<BudgetType>> RepositoryMock { get; }
        public override BudgetTypeService Service =>
            new BudgetTypeService(RepositoryMock.Object, Mapper);

        public BudgetTypeServiceTests()
        {
            RepositoryMock = new Mock<IBudgetRepository<BudgetType>>(MockBehavior.Strict);
            FakeModel = new BudgetType
            {
                Id = Guid.NewGuid(),
                Name = "string",
                UserId = Guid.NewGuid()
            };
            FakeDTO = new BudgetTypeDTO
            {
                Id = Guid.NewGuid(),
                Name = "string",
            };
        }

        [Fact]
        public void GetAll_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => new List<BudgetType> { FakeModel });

            var budgetTypes = Service.GetAll().Result;

            Assert.Single(budgetTypes);
        }

    }
}
