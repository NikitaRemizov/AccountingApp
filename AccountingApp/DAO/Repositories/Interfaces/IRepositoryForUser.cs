using DAO.Models;
using System;

namespace DAO.Repositories.Interfaces
{
    public interface IRepositoryForUser<T> : IRepository<T> where T : Model
    {
        void SetUser(Guid id);
    }
}
