using AutoMapper;
using AccountingApp.DAL.Models;

namespace AccountingApp.DAL.Utils
{
    internal class DAOModelsMapperProfile : Profile
    {
        public DAOModelsMapperProfile()
        {
            CreateMap<User, User>();
            CreateMap<BudgetChange, BudgetChange>()
                .ForMember(b => b.User, opt => opt.Ignore())
                .ForMember(b => b.UserId, opt => opt.Ignore());
            CreateMap<BudgetType, BudgetType>()
                .ForMember(b => b.User, opt => opt.Ignore())
                .ForMember(b => b.UserId, opt => opt.Ignore());
        }
    }
}
