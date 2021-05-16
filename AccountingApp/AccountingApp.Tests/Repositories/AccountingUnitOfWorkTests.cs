using AccountingApp.DAO.Repositories;
using AccountingApp.DAO.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace AccountingApp.Tests.Repositories
{
    public class AccountingUnitOfWorkTests
    {
        [Fact]
        public void Dispose_CalledOnce_DbContextDisposeMustBeCalledOnce()
        {
            var dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            dbContextMock
                .Setup(db => db.Dispose());
            var unitOfWork = new AccountingUnitOfWork(dbContextMock.Object);

            unitOfWork.Dispose();

            dbContextMock.Verify(db => db.Dispose(), Times.Once);
        }

        [Fact]
        public void Dispose_CalledTwice_DbContextDisposeMustBeCalledOnce()
        {
            var dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            dbContextMock
                .Setup(db => db.Dispose());
            var unitOfWork = new AccountingUnitOfWork(dbContextMock.Object);

            unitOfWork.Dispose();
            unitOfWork.Dispose();

            dbContextMock.Verify(db => db.Dispose(), Times.Once);
        }

        [Fact]
        public void Register_RepositoryIsNotRegisteredInUnitOfWork_CallsRepositorySetDbContext()
        {
            var dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            var repostoryMock = new Mock<IRepository>(MockBehavior.Strict);
            repostoryMock
                .Setup(db => db.SetDbContext(It.IsNotNull<DbContext>()));
            var unitOfWork = new AccountingUnitOfWork(dbContextMock.Object);

            (unitOfWork as IAccountingUnitOfWork).Register(repostoryMock.Object);

            repostoryMock.Verify(r => r.SetDbContext(It.IsNotNull<DbContext>()), Times.Once);
        }

        [Fact]
        public void Register_RepositoryIsRegisteredInUnitOfWork_ThrowsInvalidOperationException()
        {
            var dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            var repostoryMock = new Mock<IRepository>(MockBehavior.Strict);
            repostoryMock
                .Setup(db => db.SetDbContext(It.IsNotNull<DbContext>()));
            var unitOfWork = new AccountingUnitOfWork(dbContextMock.Object);

            (unitOfWork as IAccountingUnitOfWork).Register(repostoryMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
            {
                (unitOfWork as IAccountingUnitOfWork).Register(repostoryMock.Object);
            });
            repostoryMock.Verify(r => r.SetDbContext(It.IsNotNull<DbContext>()), Times.Once);
        }
    }
}
