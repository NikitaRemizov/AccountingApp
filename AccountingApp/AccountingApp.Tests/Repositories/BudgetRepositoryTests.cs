using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using AccountingApp.Tests.Repositories.Utils;
using System;
using System.Linq;
using Xunit;

namespace AccountingApp.Tests.Repositories
{
    public abstract class BudgetRepositoryTests<T> : AccountingRepositoryTests<T> where T : BudgetModel, new()
    {
        protected override IBudgetRepository<T> Repository { get; }

        [Fact]
        public virtual void Get_SetOtherUserWithoutRecordsInDatabase_UserCannotAccessOtherUserRecords()
        {
            Repository.SetUser(TestData.UserWithoutRecords.Email);
            var result = Repository.Get(TestModel.First().Id).Result;
            Assert.Null(result);
        }

        [Fact]
        public virtual void Update_SetOtherUserWithoutRecordsInDatabase_UserCannotAccessOtherUserRecords()
        {
            var budgetModel = Repository.GetAll().Result.First();
            Repository.SetUser(TestData.UserWithoutRecords.Email);
            var id = Repository.Update(budgetModel).Result;
            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public virtual void Delete_SetOtherUserWithoutRecordsInDatabase_UserCannotAccessOtherUserRecords()
        {
            var budgetModel = Repository.GetAll().Result.First();
            Repository.SetUser(TestData.UserWithoutRecords.Email);
            var id = Repository.Delete(budgetModel.Id).Result;
            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Get_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.Get(Guid.NewGuid()));
        }

        [Fact]
        public void GetAll_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.GetAll());
        }

        [Fact]
        public void Find_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.Find((x) => true));
        }

        [Fact]
        public void Create_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.Create(new T()));
        }

        [Fact]
        public void Update_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.Update(new T()));
        }

        [Fact]
        public void Delete_InvalidSetUserCallBeforeMethodInvocation_ThrowsException()
        {
            SetInvalidUserAndCatch();
            Assert.ThrowsAnyAsync<Exception>(
                () => Repository.Delete(Guid.NewGuid()));
        }

        protected void SetInvalidUserAndCatch()
        {
            try
            {
                Repository.SetUser(Guid.NewGuid().ToString()).Wait();
            }
            catch (Exception)
            {
            }
        }
    }
}
