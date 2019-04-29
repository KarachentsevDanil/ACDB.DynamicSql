using ACDB.DynamicSql.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACDB.DynamicSql.DAL.Extensions
{
    public static class GameExtension
    {
        public static void BuildGameModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Category)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Company)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .Property(g => g.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(g => g.CompanyId)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(g => g.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Game>()
                .Property(g => g.Price)
                .IsRequired();
        }
    }
}