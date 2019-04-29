using ACDB.DynamicSql.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACDB.DynamicSql.DAL.Extensions
{
    public static class CategoryExtension
    {
        public static void BuildCategoryModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Category>()
                .Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .Property(g => g.Description)
                .HasMaxLength(500);
        }
    }
}