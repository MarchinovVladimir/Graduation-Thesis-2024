using AIO.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AIO.Data
{
	/// <summary>
	/// The database context for the application.
	/// </summary>
	public class AIODbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the AIODbContex class.
        /// </summary>
        public AIODbContext(DbContextOptions<AIODbContext> options)
            : base(options)
        {
           
        }

		/// <summary>
		/// DbSet<Product> Products table in the database.
		/// </summary>
		public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// DbSet<Category> Categories table in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;

        /// <summary>
        /// DbSet<LocationArea> LocationAreas table in the database.
        /// </summary>
        public DbSet<LocationArea> LocationAreas { get; set; } = null!;

        /// <summary>
        /// DbSet<Agent> Sellers table in the database.
        /// </summary>
        public DbSet<Seller> Sellers { get; set;} = null!;

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(AIODbContext)) ?? Assembly.GetExecutingAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
            base.OnModelCreating(modelBuilder);        
        }
    }
}