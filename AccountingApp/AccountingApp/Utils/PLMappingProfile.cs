using AccountingApp.Models;
using AccountingApp.BLL.DTO;

namespace AccountingApp.Utils
{
    public class PLMappingProfile : AutoMapper.Profile
    {
        public PLMappingProfile()
        {
            CreateMap<BudgetChange, BudgetChangeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<BudgetType, BudgetTypeDTO>().ReverseMap();
        }
    }
}
