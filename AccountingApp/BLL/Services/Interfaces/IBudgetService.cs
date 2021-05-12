using BLL.DTO;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBudgetService<T> : IDisposable where T : BudgetDTO
    {
        Task SetUser(string email);
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}
