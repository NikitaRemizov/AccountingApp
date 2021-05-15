using AccountingApp.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace AccountingApp.BLL.Services.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<bool> IsRegistered(UserDTO user);
        Task<Guid> Register(UserDTO user);
        public Task<Guid?> VerifyCredentials(UserDTO user);
    }
}
