using AccountingApp.DAL.Models;
using AccountingApp.DAL.Repositories;
using AccountingApp.DAL.Repositories.Interfaces;
using Xunit;
using Moq;
using AccountingApp.Tests.Repositories.Utils;
using System.Collections.Generic;

namespace AccountingApp.Tests.Repositories
{
    public class UserRepositoryTests : AccountingRepositoryTests<User>
    {
        protected override IRepository<User> Repository { get; }
        protected override IEnumerable<User> TestModel { get; }

        public UserRepositoryTests()
        {
            Repository = new UserRepository(UnitOfWork);
            TestModel = new List<User> { TestData.User };
        }

        [Fact]
        public void AccountingRepository_CallsUnitOfWorkRegister()
        {
            var unitOfWorkMock = new Mock<IAccountingUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock
                .Setup(u => u.Register(It.IsNotNull<UserRepository>()));
            unitOfWorkMock
                .Setup(u => u.Dispose());

            var repository = new UserRepository(unitOfWorkMock.Object);

            unitOfWorkMock.Verify(u => u.Register(It.IsNotNull<IRepository>()), Times.Once);
        }
    }
}
