using AccountingApp.Models;
using BLL.DTO;

namespace AccountingApp.Utils
{
    public class PLMappingProfile : AutoMapper.Profile
    {
        public PLMappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
