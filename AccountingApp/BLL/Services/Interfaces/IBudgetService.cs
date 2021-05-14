using BLL.DTO;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBudgetService<T> : IDisposable where T : BudgetDTO
    {
        Task SetUser(string email);
        Task<Guid> Create(T item);
        Task<Guid> Update(T item);
        Task<Guid> Delete(Guid id);
    }
}
