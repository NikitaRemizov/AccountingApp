using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.DAO.Models;

namespace AccountingApp.BLL.Utils
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(user => user.Password, opt =>
                {
                    opt.MapFrom(user => new Password(user.Password).StoredHash);
                });

            CreateMap<BudgetType, BudgetTypeDTO>()
                .ReverseMap()
                .ForMember(b => b.User, opt => opt.Ignore())
                .ForMember(b => b.UserId, opt => opt.Ignore());

            CreateMap<BudgetType, BudgetChangeDTO>()
                .ForMember(bc => bc.BudgetTypeName, opt => opt.MapFrom(bt => bt.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BudgetChange, BudgetChangeDTO>()
                .AfterMap((bc, dto, context) => context.Mapper.Map<BudgetChangeDTO>(dto));

            CreateMap<BudgetChangeDTO, BudgetChange>()
                .ForMember(bc => bc.User, opt => opt.Ignore())
                .ForMember(bc => bc.UserId, opt => opt.Ignore())
                .ForMember(bc => bc.BudgetType, opt => opt.Ignore());
        }
    }
}
