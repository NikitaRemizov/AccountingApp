using AccountingApp.BLL.Utils;
using AccountingApp.DAL.Databases;
using AccountingApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AccountingApp.Tests.Repositories.Utils
{
    internal static class TestData
    {
        private static readonly string _databaseName = Guid.NewGuid().ToString();
        public static User User { get; }
        public static User UserWithoutRecords { get; }
        public static List<BudgetType> BudgetTypes { get; }
        public static List<BudgetChange> BudgetChanges { get; }
        static TestData()
        {
            User = new User
            {
                Id = Guid.NewGuid(),
                Email = "email1",
                Password = new byte[Password.StoredHashSize]
            };

            UserWithoutRecords = new User
            {
                Id = Guid.NewGuid(),
                Email = "email2",
                Password = new byte[Password.StoredHashSize]
            };

            var type1 = new BudgetType
            {
                Id = Guid.NewGuid(),
                Name = "type1",
                User = User,
                UserId = User.Id,
            };
            var type2 = new BudgetType
            {
                Id = Guid.NewGuid(),
                Name = "type2",
                User = User,
                UserId = User.Id,
            };

            BudgetTypes = new List<BudgetType> { type1, type2 };

            BudgetChanges = new List<BudgetChange>
            {
                new BudgetChange
                {
                    Id = Guid.NewGuid(),
                    Amount = 10,
                    BudgetType = type1,
                    BudgetTypeId = type1.Id,
                    Date = DateTime.Today,
                    User = User,
                    UserId = User.Id
                },
                new BudgetChange
                {
                    Id = Guid.NewGuid(),
                    Amount = -10,
                    BudgetType = type2,
                    BudgetTypeId = type2.Id,
                    Date = DateTime.Today,
                    User = User,
                    UserId = User.Id
                },
            };
        }

        internal static AccountingDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AccountingDbContext>()
                .UseInMemoryDatabase(_databaseName)
                .Options;
            return new AccountingDbContext(options);
        }

        internal static void InitDbContext()
        {
            using var dbContext = CreateDbContext();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            dbContext.Set<User>().AddRange(User, UserWithoutRecords);
            dbContext.Set<BudgetType>().AddRange(BudgetTypes);
            dbContext.Set<BudgetChange>().AddRange(BudgetChanges);
            dbContext.SaveChanges();
        }
    }
}
