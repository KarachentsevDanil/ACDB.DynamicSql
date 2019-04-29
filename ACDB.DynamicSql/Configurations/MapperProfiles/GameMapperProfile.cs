using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DTOs.Categories;
using ACDB.DynamicSql.DTOs.Companies;
using ACDB.DynamicSql.DTOs.Games;
using AutoMapper;

namespace ACDB.DynamicSql.Configurations.MapperProfiles
{
    public class GameMapperProfile : Profile
    {
        public GameMapperProfile()
        {
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Category, GetCategoryDto>();

            CreateMap<CreateCompanyDto, Company>();

            CreateMap<UpdateCompanyDto, Company>();

            CreateMap<Company, GetCompanyDto>();

            CreateMap<CreateGameDto, Game>();

            CreateMap<UpdateGameDto, Game>();

            CreateMap<Game, GetGameDto>();
        }
    }
}