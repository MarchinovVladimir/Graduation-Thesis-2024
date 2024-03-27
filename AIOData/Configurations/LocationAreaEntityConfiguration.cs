using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
	/// <summary>
	/// Location area entity configuration.
	/// </summary>
	public class LocationAreaEntityConfiguration : IEntityTypeConfiguration<LocationArea>
	{
		/// <summary>
		/// Method for configuring location area entity.
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<LocationArea> builder)
		{
			builder
				.HasData(GenerateLocationAreas());
		}

		/// <summary>
		/// Method for generating location areas.
		/// </summary>
		/// <returns></returns>
		private LocationArea[] GenerateLocationAreas()
		{
			ICollection<LocationArea> locationAreas = new HashSet<LocationArea>();

			LocationArea locationArea;

			locationArea = new LocationArea
			{
				Id = 1,
				Name = "Sofia",
				PostCode = "1000"
			};
			locationAreas.Add(locationArea);

			locationArea = new LocationArea
			{
				Id = 2,
				Name = "Plovdiv",
				PostCode = "4000"
			};
			locationAreas.Add(locationArea);

			locationArea = new LocationArea
			{
				Id = 3,
				Name = "Varna",
				PostCode = "9000"
			};
			locationAreas.Add(locationArea);

			locationArea = new LocationArea
			{
				Id = 4,
				Name = "Burgas",
				PostCode = "8000"
			};
			locationAreas.Add(locationArea);

			return locationAreas.ToArray();
		}
	}
}
