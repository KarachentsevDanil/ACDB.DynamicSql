using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ACDB.DynamicSql.DAL.Context
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildCategoryModel();
            modelBuilder.BuildCompanyModel();
            modelBuilder.BuildGameModel();
        }
    }
}