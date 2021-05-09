using AutoMapper;
using BLL.DTO;
using DAO.Models;

namespace BLL.Utils
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            var password = 
            CreateMap<UserDTO, User>()
                .ForMember(user => user.Password, opt =>
                {
                    opt.MapFrom(user => new Password(user.Password).StoredHash);
                });
        }
    }
}
