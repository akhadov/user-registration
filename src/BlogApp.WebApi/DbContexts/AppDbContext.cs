using RegistrUser.WebApi.Enums;
using RegistrUser.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace RegistrUser.WebApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<RegistrUser>().HasData(new RegistrUser
            //{
            //    FirstName = "Jon",
            //    LastName = "Doe",
            //    UserRole = UserRole.Admin,
            //    PasswordHash = "string",
            //    CreatedAt = DateTime.UtcNow,
            //    Email = "string",
            //    ImagePath = "string",
            //    IsEmailConfirmed = false,
            //    Salt = Guid.NewGuid().ToString(),
            //    UserName = "admin",
            //});
        }

        public virtual DbSet<User> Users { get; set; } = null!;
    }
}