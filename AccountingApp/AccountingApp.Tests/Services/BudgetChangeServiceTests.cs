using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services;
using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories.Interfaces;
using AccountingApp.DAL.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Tests.Services
{
    public class BudgetChangeServiceTests : BudgetServiceTests<BudgetChangeDTO, BudgetChange>
    {
        public override BudgetChange FakeModel { get; }
        public override BudgetChangeDTO FakeDTO { get; }
        public override Mock<IBudgetRepository<BudgetChange>> RepositoryMock { get; }
        public override BudgetChangeService Service =>
            new BudgetChangeService(RepositoryMock.Object, Mapper);

        public BudgetChangeServiceTests()
        {
            RepositoryMock = new Mock<IBudgetRepository<BudgetChange>>(MockBehavior.Strict);
            FakeModel = new BudgetChange
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                BudgetTypeId = Guid.NewGuid(),
                Date = DateTime.Today,
                UserId = Guid.NewGuid()
            };
            FakeDTO = new BudgetChangeDTO
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                BudgetTypeId = Guid.NewGuid(),
                Date = DateTime.Today,
            };
        }

        [Fact]
        public void GetForDate_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<BudgetChange, bool>>>()))
                .ReturnsAsync(() => new List<BudgetChange> { FakeModel });

            var budgetTypes = Service.GetForDate(DateTime.Today).Result;

            Assert.Single(budgetTypes);
        }

        [Fact]
        public void GetBetweenDates_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<BudgetChange, bool>>>()))
                .ReturnsAsync(() => new List<BudgetChange> { FakeModel });

            var budgetTypes = Service.GetForDate(DateTime.Today).Result;

            Assert.Single(budgetTypes);
        }

        [Fact]
        public void Create_RepositoryCreateOrSaveThrownsInvalidEntityException_ReturnsGuidEmpty()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<BudgetChange>()))
                .ThrowsAsync(new InvalidEntityException(string.Empty, null));
            RepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Create(FakeDTO).Result;

            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Update_RepositoryCreateOrSaveThrownsInvalidEntityException_ReturnsGuidEmpty()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<BudgetChange>()))
                .ThrowsAsync(new InvalidEntityException(string.Empty, null));
            RepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Update(FakeDTO).Result;

            Assert.Equal(Guid.Empty, id);
        }
    }
}
