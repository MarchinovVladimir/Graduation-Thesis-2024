using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
	/// <summary>
	/// The configuration for the ApplicationUser entity.
	/// </summary>
	public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		/// <summary>
		/// Configures the ApplicationUser entity. 
		/// If the user's first name and last name are not provided, they will be set to "Test".
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(u => u.FirstName)
				.HasDefaultValue("Test");

			builder
				.Property(u => u.LastName)
				.HasDefaultValue("Test");
		}
	}
}
