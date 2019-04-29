using ACDB.DynamicSql.Configurations;
using ACDB.DynamicSql.Configurations.MapperProfiles;
using ACDB.DynamicSql.DAL.Context;
using ACDB.DynamicSql.DAL.Repositories;
using ACDB.DynamicSql.DAL.Repositories.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACDB.DynamicSql.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static void RegisterDbConfiguration(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            AzureSqlConfiguration azureSqlConfiguration = new AzureSqlConfiguration();
            configuration.Bind("AzureSqlConfiguration", azureSqlConfiguration);

            serviceCollection.AddSingleton(azureSqlConfiguration);

            serviceCollection.AddDbContext<GameContext>(
                options =>
                    options.UseSqlServer(azureSqlConfiguration.ConnectionString));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<GameContext>();
            dbContextOptionsBuilder.UseSqlServer(azureSqlConfiguration.ConnectionString);

            using (var dbContext = new GameContext(dbContextOptionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
            }
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGameRepository, GameRepository>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddTransient<ICompanyRepository, CompanyRepository>();
        }

        public static void RegisterMapper(this IServiceCollection serviceCollection)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<GameMapperProfile>();
            });

            serviceCollection.AddSingleton(s => mapperConfiguration.CreateMapper());
        }
    }
}