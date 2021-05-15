using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.Controllers;
using AccountingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountingApp.Tests.Controllers
{
    public class BudgetChangeControllerTests : BudgetControllerTests<BudgetChangeDTO, BudgetChange>
    {
        protected override BudgetChangeController Controller => 
            new BudgetChangeController(ServiceMock.Object, Mapper);
        protected override Mock<IBudgetChangeService<BudgetChangeDTO>> ServiceMock { get; }

        protected override BudgetChange FakeModel { get; }

        public BudgetChangeControllerTests()
        {
            ServiceMock = new Mock<IBudgetChangeService<BudgetChangeDTO>>(MockBehavior.Strict);

            FakeModel = new BudgetChange
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                BudgetTypeId = Guid.NewGuid(),
                BudgetTypeName = "string",
                Date = DateTime.Today
            };
        }

        [Fact]
        public void GetForDate_ReturnsOkObjectResult()
        {
            ServiceMock
                .Setup(b => b.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO>());

            var result = Controller.GetForDate(DateTime.Today).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetForDate_ObjectResultIsIEnumerableBudgetChange()
        {
            ServiceMock
                .Setup(b => b.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO>());

            var result = Controller.GetForDate(DateTime.Today).Result as ObjectResult;
            var objectResult = result.Value;

            Assert.True(objectResult is IEnumerable<BudgetChange>);
        }

        [Fact]
        public void GetForDate_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            ServiceMock
                .Setup(b => b.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO> { new BudgetChangeDTO() });

            var result = Controller.GetForDate(DateTime.Today).Result as ObjectResult;
            var objectResult = result.Value as IEnumerable<BudgetChange>;

            Assert.Single(objectResult);
        }

        [Fact]
        public void GetBetweenDates_ReturnsOkObjectResult()
        {
            ServiceMock
                .Setup(b => b.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO>());

            var result = Controller.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetBetweenDates_ObjectResultIsIEnumerableBudgetChange()
        {
            ServiceMock
                .Setup(b => b.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO>());

            var result = Controller.GetBetweenDates(DateTime.Today, DateTime.Today).Result as ObjectResult;
            var objectResult = result.Value;

            Assert.True(objectResult is IEnumerable<BudgetChange>);
        }

        [Fact]
        public void GetBetweenDates_ObjectResultContainsSameNumberOfElementsAsServiceProvided()
        {
            ServiceMock
                .Setup(b => b.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => new List<BudgetChangeDTO> { new BudgetChangeDTO() });

            var result = Controller.GetBetweenDates(DateTime.Today, DateTime.Today).Result as ObjectResult;
            var objectResult = result.Value as IEnumerable<BudgetChange>;

            Assert.Single(objectResult);
        }
    }
}
