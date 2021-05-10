using AutoMapper;
using DAO.Models;

namespace DAO.Utils
{
    internal class DAOModelsMapperProfile : Profile
    {
        public DAOModelsMapperProfile()
        {
            CreateMap<User, User>();
            CreateMap<BudgetChange, BudgetChange>();
            CreateMap<BudgetType, BudgetType>();
        }
    }
}
