using AIO.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AIO.Data
{
    public class AIODbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AIODbContext(DbContextOptions<AIODbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Agent> Agents { get; set;} = null!;

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(AIODbContext)) ?? Assembly.GetExecutingAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
            base.OnModelCreating(modelBuilder);        
        }
    }
}