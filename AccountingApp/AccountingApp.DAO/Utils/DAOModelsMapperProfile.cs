using AutoMapper;
using AccountingApp.DAO.Models;

namespace AccountingApp.DAO.Utils
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
