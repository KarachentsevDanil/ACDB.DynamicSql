using ACDB.DynamicSql.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACDB.DynamicSql.DAL.Extensions
{
    public static class CompanyExtension
    {
        public static void BuildCompanyModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Company>()
                .Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Company>()
                .Property(g => g.Description)
                .HasMaxLength(500);
        }
    }
}