using BLL.DTO;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBudgetService<T> : IDisposable where T : BudgetDTO
    {
        Task SetUser(Guid id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}
