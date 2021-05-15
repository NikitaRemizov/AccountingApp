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
    public class AccountControllerTests
    {
        private static AuthentificationOptions AuthOptions { get; }         
        private static IMapper Mapper { get; }
        private static User FakeUser { get; }        
        private Mock<IAccountService> ServiceMock { get; }
        private AccountController Controller =>
            new AccountController(ServiceMock.Object, Mapper, AuthOptions);

        static AccountControllerTests()
        {
            AuthOptions = new AuthentificationOptions
            {
                Audience = "string",
                Issuer = "string",
                Lifetime = 10,
            };
            AuthOptions.GenerateKey();
            FakeUser =  new User
            {
                Email = "string",
                Password = "string"
            };

            Mapper = new MapperConfiguration(configure =>
            {
                configure.AddProfile<PLMappingProfile>();
            }).CreateMapper();
        }

        public AccountControllerTests()
        {
            ServiceMock = new Mock<IAccountService>(MockBehavior.Strict);
        }

        [Fact]
        public void Login_ProvidedUserIsNull_ReturnsBadRequest()
        {
            var result = Controller.Login(null).Result;

            ServiceMock.Verify();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_ProvidedUserDoesNotExist_ReturnsBadRequest()
        {
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => null);

            var result = Controller.Login(FakeUser).Result;

            ServiceMock.Verify();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_ProvidedUserExist_ReturnsOk()
        {
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Login(FakeUser).Result;

            ServiceMock.Verify();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_ProvidedUserExist_ResponseBodyContainsJwtTokenObjectResult()
        {
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Login(FakeUser).Result;
            var resultObject = (result as OkObjectResult).Value;

            ServiceMock.Verify();
            Assert.IsType<AccountController.JwtTokenResponse>(resultObject);
        }

        [Fact]
        public void Login_ProvidedUserExist_JwtTokenObjectResultContainsTokenAndEmail()
        {
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Login(FakeUser).Result;
            var resultObject = (result as OkObjectResult).Value as AccountController.JwtTokenResponse;

            ServiceMock.Verify();
            Assert.NotNull(resultObject.AccessToken);
            Assert.NotEmpty(resultObject.AccessToken);
            Assert.Equal(FakeUser.Email, resultObject.Email);
        }

        [Fact]
        public void Register_ProvidedUserIsNull_ReturnsBadRequest()
        {
            var result = Controller.Login(null).Result;

            ServiceMock.Verify();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Register_ProvidedUserIsAlreadyRegistered_ReturnsConflict()
        {
            ServiceMock
                .Setup(a => a.IsRegistered(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => true);

            var result = Controller.Register(FakeUser).Result;

            ServiceMock.Verify();
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void Register_ProvidedUserIsNotRegistered_RegisterIsCalled()
        {
            ServiceMock
                .Setup(a => a.IsRegistered(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => false);
            ServiceMock
                .Setup(a => a.Register(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.Empty);

            var result = Controller.Register(FakeUser).Result;

            ServiceMock.Verify();
            ServiceMock.Verify(a => a.Register(It.IsNotNull<UserDTO>()), Times.Once);
        }

        [Fact]
        public void Register_RegisterReturnsGuidEmpty_ReturnsStatusCode500()
        {
            ServiceMock
                .Setup(a => a.IsRegistered(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => false);
            ServiceMock
                .Setup(a => a.Register(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.Empty);

            var result = Controller.Register(FakeUser).Result as ObjectResult;

            ServiceMock.Verify();
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public void Register_RegisterReturnsValidGuid_ResponseBodyContainsJwtTokenObjectResult()
        {
            ServiceMock
                .Setup(a => a.IsRegistered(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => false);
            ServiceMock
                .Setup(a => a.Register(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Register(FakeUser).Result;
            var resultObject = (result as OkObjectResult).Value;

            ServiceMock.Verify();
            Assert.IsType<AccountController.JwtTokenResponse>(resultObject);
        }

        [Fact]
        public void Register_RegisterReturnsValidGuid_JwtTokenObjectResultContainsTokenAndEmail()
        {
            ServiceMock
                .Setup(a => a.IsRegistered(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => false);
            ServiceMock
                .Setup(a => a.Register(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());
            ServiceMock
                .Setup(a => a.VerifyCredentials(It.IsNotNull<UserDTO>()))
                .ReturnsAsync(() => Guid.NewGuid());

            var result = Controller.Register(FakeUser).Result;
            var resultObject = (result as OkObjectResult).Value as AccountController.JwtTokenResponse;

            ServiceMock.Verify();
            Assert.NotNull(resultObject.AccessToken);
            Assert.NotEmpty(resultObject.AccessToken);
            Assert.Equal(FakeUser.Email, resultObject.Email);
        }
    }
}
