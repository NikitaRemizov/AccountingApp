using BLL.DTO;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<Guid> GetId(UserDTO user);
        Task<bool> IsRegistered(UserDTO user);
        Task<Guid> Register(UserDTO user);
        public Task<Guid?> VerifyCredentials(UserDTO user);
    }
}
