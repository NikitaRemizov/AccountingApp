using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
using AccountingApp.BLL.Utils;
using AccountingApp.DAO.Models;
using AccountingApp.DAO.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public AccountService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Guid?> VerifyCredentials(UserDTO user)
        {
            var userWithSameEmail = await GetUserWithEmail(user.Email);
            if (userWithSameEmail is null)
            {
                return null;
            }
            var password = new Password(user.Password, userWithSameEmail.Password).StoredHash;
            if (!userWithSameEmail.Password.SequenceEqual(password))
            {
                return null;
            }
            return userWithSameEmail.Id;
        }

        public async Task<Guid> Register(UserDTO user)
        {
            var createdUser = await _repository.Create(_mapper.Map<User>(user));
            if (createdUser is null)
            {
                return Guid.Empty;
            }
            await _repository.Save();
            return createdUser.Id;
        }

        public async Task<bool> IsRegistered(UserDTO user)
        {
            return (await GetUserWithEmail(user.Email)) is not null;
        }

        private async Task<User> GetUserWithEmail(string email)
        {
            var users = await _repository.Find(u => u.Email == email);
            var userWithSameEmail = users.SingleOrDefault();
            if (userWithSameEmail is null)
            {
                return null;
            }
            return userWithSameEmail;
        }
    }
}
