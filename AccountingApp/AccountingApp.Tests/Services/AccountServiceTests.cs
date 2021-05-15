using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services;
using AccountingApp.BLL.Utils;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Tests.Services
{
    public class AccountServiceTests
    {
        public static IMapper Mapper { get; set; }
        public static User FakeUser { get; set; }
        public static UserDTO FakeUserDTO { get; set; }
        public Mock<IRepository<User>> RepositoryMock { get; }
        public AccountService Service { 
            get
            {
                var service = new AccountService(RepositoryMock.Object, Mapper);
                return service;
            } 
        } 
            
        static AccountServiceTests()
        {
            Mapper = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<BLLMappingProfile>();
            }).CreateMapper();
            FakeUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "string",
                Password = new byte[36]
            };
            FakeUserDTO = new UserDTO
            {
                Email = "string",
                Password = "string"
            };
        }

        public AccountServiceTests()
        {
            RepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
        }

        [Fact]
        public void Dispose_MustCallRepositoryDispose()
        {
            RepositoryMock
                .Setup(r => r.Dispose())
                .Verifiable();

            Service.Dispose();

            RepositoryMock.Verify(r => r.Dispose(), Times.Once);
        }

        [Fact]
        public void Register_RepositoryCreateReturnsNull_ReturnsGuidEmpty()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(() => null);

            var userId = Service.Register(null).Result;

            RepositoryMock.Verify();
            Assert.Equal(Guid.Empty, userId);
        }

        [Fact]
        public void Register_RepositoryCreateReturnsValidUserObject_CallsSaveAndReturnsId()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(() => FakeUser);
            RepositoryMock
                .Setup(r => r.Save())
                .Returns(() => Task.CompletedTask);

            var userId = Service.Register(null).Result;

            RepositoryMock.Verify(r => r.Save(), Times.Once);
            Assert.NotEqual(Guid.Empty, userId);
        }

        [Fact]
        public void IsRegistered_RepositoryContainsRequestedUser_ReturnsTrue()
        {
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new List<User> { FakeUser });

            var isRegistered = Service.IsRegistered(FakeUserDTO).Result;

            Assert.True(isRegistered);
        }

        [Fact]
        public void IsRegistered_RepositoryDoesNotContainRequestedUser_ReturnsTrue()
        {
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new List<User> { });

            var isRegistered = Service.IsRegistered(FakeUserDTO).Result;

            Assert.False(isRegistered);
        }

        [Fact]
        public void VerifyCredentials_RepositoryContainsRequestedUserAndPasswordCorrect_ReturnsTrue()
        {
            FakeUser.Password = new Password(FakeUserDTO.Password).StoredHash;
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new List<User> { FakeUser });

            var userId = Service.VerifyCredentials(FakeUserDTO).Result;

            Assert.NotNull(userId);
        }

        [Fact]
        public void VerifyCredentials_RepositoryContainsRequestedUserAndPasswordIsWrong_ReturnsTrue()
        {
            FakeUser.Password = new Password(FakeUserDTO.Password).StoredHash;
            FakeUser.Password[0]++;
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new List<User> { FakeUser });

            var userId = Service.VerifyCredentials(FakeUserDTO).Result;

            Assert.Null(userId);
        }

        [Fact]
        public void VerifyCredentials_RepositoryDoesNotContainsRequestedUser_ReturnsTrue()
        {
            FakeUser.Password = new Password(FakeUserDTO.Password).StoredHash;
            RepositoryMock
                .Setup(r => r.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new List<User> { });

            var userId = Service.VerifyCredentials(FakeUserDTO).Result;

            Assert.Null(userId);
        }
    }
}
