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

            CreateMap<BudgetType, BudgetTypeDTO>()
                .ReverseMap()
                .ForMember(b => b.User, opt => opt.Ignore())
                .ForMember(b => b.UserId, opt => opt.Ignore());

            CreateMap<BudgetType, BudgetChangeDTO>()
                .ForMember(bc => bc.BudgetTypeId, opt => opt.MapFrom(bt => bt.Id))
                .ForMember(bc => bc.BudgetTypeName, opt => opt.MapFrom(bt => bt.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BudgetChange, BudgetChangeDTO>()
                .AfterMap((src, dest, context) => context.Mapper.Map<BudgetChangeDTO>(dest));

            CreateMap<BudgetChangeDTO, BudgetChange>()
                .ForMember(bc => bc.User, opt => opt.Ignore())
                .ForMember(bc => bc.UserId, opt => opt.Ignore())
                .ForMember(bc => bc.BudgetType, opt => opt.Ignore());
                //.AfterMap((src, dest, context) => context.Mapper.Map<BudgetChange>(dest))
                //.ForPath(bc => bc.BudgetType, conf => conf.MapFrom(dto => dto));
                //.ForMember(b => b.BudgetType, opt => opt.Ignore());
        }
    }
}
