using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.Tests.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace AccountingApp.Tests.Repositories
{
    [Collection("RepositoryTests")]
    public abstract class AccountingRepositoryTests<T> where T : Model, new()
    {
        protected abstract IRepository<T> Repository { get; }
        private protected AccountingUnitOfWork UnitOfWork { get; }

        protected abstract IEnumerable<T> TestModel { get; }

        public AccountingRepositoryTests()
        {
            TestData.InitDbContext();
            var dbContext = TestData.CreateDbContext();
            UnitOfWork = new AccountingUnitOfWork(dbContext);
        }

        [Fact]
        public virtual void Get_NonExistingId_ReturnsNull()
        {
            var id = Guid.NewGuid();
            var model = Repository.Get(id).Result;
            Assert.Null(model);
        }

        [Fact]
        public virtual void Get_ExistingGuid_ReturnsCorrectModel()
        {
            var id = TestModel.First().Id;
            var model = Repository.Get(id).Result;
            Assert.Equal(TestModel.First().Id, model.Id);
        }

        [Fact]
        public virtual void GetAll_ReturnsListOfModels()
        {
            var model = Repository.GetAll().Result;
            Assert.True(AreAllSame(TestModel, model));
        }

        [Fact]
        public virtual void Find_PredicateMathesExistingId_ReturnsListContainingCorrespondingObject()
        {
            var model = Repository.Find(m => m.Id == TestModel.First().Id).Result;
            Assert.Equal(model.First().Id, TestModel.First().Id);
        }

        [Fact]
        public virtual void Find_PredicateDoesNotMathExistingId_ReturnsEmptyList()
        {
            var model = Repository.Find(m => m.Id == Guid.NewGuid()).Result;
            Assert.Empty(model);
        }

        [Fact]
        public virtual void Create_CreateNewEntity_ReturnsThatEntity()
        {
            var newModel = new T();
            var createdModel = Repository.Create(newModel).Result;
            Repository.Save().Wait();
            var createdModelFromDb = Repository.Get(createdModel.Id).Result;
            Assert.Equal(newModel.Id, createdModel.Id);
            Assert.Equal(newModel.Id, createdModelFromDb.Id);
        }

        [Fact]
        public virtual void Delete_NonExistingIdAsParameter_ReturnsGuidEmpty()
        {
            var id = Repository.Delete(Guid.NewGuid()).Result;
            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public virtual void Delete_ExistingIdAsParameter_DeletesCorrespondingEntity()
        {
            var id = Repository.Delete(TestModel.First().Id).Result;
            Repository.Save().Wait();
            var deletedEntity = Repository.Get(id).Result;
            Assert.Null(deletedEntity);
        }

        [Fact]
        public virtual void Update_NonExistingIdAsParameter_ReturnsGuidEmpty()
        {
            var id = Repository.Update(new T()).Result;
            Repository.Save().Wait();
            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public virtual void Update_ExistingEntityAsParameter_ReturnsItsGuid()
        {
            var id = Repository.Update(TestModel.First()).Result;
            Repository.Save().Wait();
            Assert.Equal(TestModel.First().Id, id);
        }

        protected static bool AreAllSame<T>(IEnumerable<T> collection1, IEnumerable<T> collection2) where T : Model
        {
            return !collection1.Where(t => !collection2.Any(m => t.Id == m.Id)).Any();
        }
    }
}
