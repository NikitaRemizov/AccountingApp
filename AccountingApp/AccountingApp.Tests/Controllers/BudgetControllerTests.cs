using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.Controllers;
using AccountingApp.Models;
using AccountingApp.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace AccountingApp.Tests.Controllers
{
    public abstract class BudgetControllerTests<TDto, TModel> where TDto : BudgetDTO where TModel : BudgetModel
    {
        protected static IMapper Mapper { get; }
        protected abstract BudgetController<TDto, TModel> Controller { get; }
        protected abstract IMock<IBudgetService<TDto>> ServiceMock { get; }
        private Mock<IBudgetService<TDto>> InternalServiceMock =>
            (ServiceMock as Mock).As<IBudgetService<TDto>>();
        protected abstract TModel FakeModel { get; }

        static BudgetControllerTests()
        {
            Mapper = new MapperConfiguration(configure =>
            {
                configure.AddProfile<PLMappingProfile>();
            }).CreateMapper();
        }

        [Fact]
        public void Create_ProvidedModelIsNull_ReturnsBadRequest()
        {
            var result = Controller.Create(null).Result;
            
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Create_ProvidedModelIsNotCreated_ReturnsNotFound()
        {
            InternalServiceMock
                .Setup(b => b.Create(It.IsNotNull<TDto>()))
                .ReturnsAsync(() => Guid.Empty);

            var result = Controller.Create(FakeModel).Result;

            InternalServiceMock.Verify();
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Create_ProvidedModelIsCreated_ReturnsOk()
        {
            InternalServiceMock
                .Setup(b => b.Create(It.IsNotNull<TDto>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Create(FakeModel).Result;

            InternalServiceMock.Verify();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Create_ProvidedModelIsCreated_OkResultContainsId()
        {
            InternalServiceMock
                .Setup(b => b.Create(It.IsNotNull<TDto>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Create(FakeModel).Result as OkObjectResult;
            var id = ((BudgetController<TDto, TModel>.IdResponse) result.Value).Id; 

            InternalServiceMock.Verify();
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public void Delete_ProvidedModelIsNotDeleted_ReturnsNotFound()
        {
            InternalServiceMock
                .Setup(b => b.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => Guid.Empty);

            var result = Controller.Delete(Guid.NewGuid()).Result;

            InternalServiceMock.Verify();
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_ProvidedModelIsDeleted_ReturnsOk()
        {
            var id = Guid.NewGuid();
            InternalServiceMock
                .Setup(b => b.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => id);

            var result = Controller.Delete(id).Result;

            InternalServiceMock.Verify();
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Update_ProvidedModelIsNull_ReturnsBadRequest()
        {
            var result = Controller.Create(null).Result;
            
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Update_ProvidedModelIsNotCreated_ReturnsNotFound()
        {
            InternalServiceMock
                .Setup(b => b.Update(It.IsNotNull<TDto>()))
                .ReturnsAsync(() => Guid.Empty);

            var result = Controller.Update(FakeModel).Result;

            InternalServiceMock.Verify();
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Update_ProvidedModelIsCreated_ReturnsOk()
        {
            InternalServiceMock
                .Setup(b => b.Update(It.IsNotNull<TDto>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Update(FakeModel).Result;

            InternalServiceMock.Verify();
            Assert.IsType<OkResult>(result);
        }
    }
}
