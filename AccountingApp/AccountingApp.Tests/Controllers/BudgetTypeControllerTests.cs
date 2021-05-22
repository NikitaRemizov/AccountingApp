using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.Controllers;
using AccountingApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountingApp.Tests.Controllers
{
    public class BudgetTypeControllerTests : BudgetControllerTests<BudgetTypeDTO, BudgetType>
    {
        protected override BudgetTypeController Controller => 
            new BudgetTypeController(ServiceMock.Object, Mapper);
        protected override Mock<IBudgetTypeService<BudgetTypeDTO>> ServiceMock { get; }

        protected override BudgetType FakeModel { get; }

        public BudgetTypeControllerTests()
        {
            ServiceMock = new Mock<IBudgetTypeService<BudgetTypeDTO>>(MockBehavior.Strict);

            FakeModel = new BudgetType
            {
                Id = Guid.NewGuid(),
                Name = "string"
            };
        }

        [Fact]
        public void GetAll_ReturnsOkObjectResult()
        {
            ServiceMock
                .Setup(b => b.GetAll())
                .ReturnsAsync(() => new List<BudgetTypeDTO>());

            var result = Controller.GetAll().Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAll_ObjectResultIsIEnumerableOfBudgetType()
        {
            ServiceMock
                .Setup(b => b.GetAll())
                .ReturnsAsync(() => new List<BudgetTypeDTO>());

            var result = Controller.GetAll().Result as OkObjectResult;
            var objectResult = result.Value;

            Assert.True(objectResult is IEnumerable<BudgetType>);
        }

        [Fact]
        public void GetAll_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            ServiceMock
                .Setup(b => b.GetAll())
                .ReturnsAsync(() => new List<BudgetTypeDTO> { new BudgetTypeDTO() });

            var result = Controller.GetAll().Result as OkObjectResult;
            var objectResult = result.Value as IEnumerable<BudgetType>;

            Assert.Single(objectResult);
        }
    }
}
