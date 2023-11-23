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
        }

        public virtual DbSet<User> Users { get; set; } = null!;
    }
}