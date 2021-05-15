using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services;
using AccountingApp.BLL.Utils;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Tests.Services
{
    public abstract class BudgetServiceTests<TDto, TModel> where TDto : BudgetDTO where TModel : BudgetModel
    {
        public static IMapper Mapper { get; }
        public abstract TModel FakeModel { get; }
        public abstract TDto FakeDTO { get; }
        public abstract IMock<IBudgetRepository<TModel>> RepositoryMock { get; }
        public abstract BudgetService<TDto, TModel> Service { get; }

        private Mock<IBudgetRepository<TModel>> InternalRepositoryMock =>
            (RepositoryMock as Mock).As<IBudgetRepository<TModel>>();
            
        static BudgetServiceTests()
        {
            Mapper = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<BLLMappingProfile>();
            }).CreateMapper();
        }

        [Fact]
        public void SetUser_MustCallRepositorySetUserOnce()
        {
            InternalRepositoryMock
                .Setup(r => r.SetUser(It.IsNotNull<string>()))
                .Returns(() => Task.CompletedTask);

            Service.SetUser(string.Empty).Wait();

            InternalRepositoryMock
                .Verify(r => r.SetUser(It.IsNotNull<string>()), Times.Once);
        }

        [Fact]
        public void Create_RepositoryCreateReturnsNull_ReturnsGuidEmpty()
        {
            InternalRepositoryMock
                .Setup(r => r.Create(It.IsAny<TModel>()))
                .ReturnsAsync(() => null);

            var id = Service.Create(FakeDTO).Result;

            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Create_RepositoryCreateReturnsModel_RepositorySaveCalledOnce()
        {
            InternalRepositoryMock
                .Setup(r => r.Create(It.IsAny<TModel>()))
                .ReturnsAsync(() => FakeModel);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Create(FakeDTO).Result;

            InternalRepositoryMock
                .Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void Create_RepositoryCreateReturnsModel_ReturnsModelId()
        {
            InternalRepositoryMock
                .Setup(r => r.Create(It.IsAny<TModel>()))
                .ReturnsAsync(() => FakeModel);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Create(FakeDTO).Result;

            Assert.Equal(FakeModel.Id, id);
        }

        [Fact]
        public void Delete_RepositoryCreateReturnsModel_RepositorySaveCalledOnce()
        {
            var idToDelete = FakeDTO.Id;
            InternalRepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => idToDelete);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Delete(idToDelete).Result;

            InternalRepositoryMock
                .Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void Delete_RepositoryCreateReturnsModel_ReturnsModelId()
        {
            var idToDelete = FakeDTO.Id;
            InternalRepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => idToDelete);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Delete(idToDelete).Result;

            Assert.Equal(idToDelete, id);
        }

        [Fact]
        public void Update_RepositoryCreateReturnsModel_RepositorySaveCalledOnce()
        {
            var idToUpdate = FakeDTO.Id;
            InternalRepositoryMock
                .Setup(r => r.Update(It.IsAny<TModel>()))
                .ReturnsAsync(() => idToUpdate);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Update(FakeDTO).Result;

            InternalRepositoryMock
                .Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void Update_RepositoryCreateReturnsModel_ReturnsModelId()
        {
            var idToUpdate = FakeDTO.Id;
            InternalRepositoryMock
                .Setup(r => r.Update(It.IsAny<TModel>()))
                .ReturnsAsync(() => idToUpdate);
            InternalRepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var id = Service.Update(FakeDTO).Result;

            Assert.Equal(idToUpdate, id);
        }
    }
}
