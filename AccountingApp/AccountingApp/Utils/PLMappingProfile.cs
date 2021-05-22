using AccountingApp.BLL.DTO;
using AccountingApp.Shared.Models;

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
