using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
	/// <summary>
	/// Configuration for the category entity.
	/// </summary>
	public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
	{

		/// <summary>
		/// Configures the category entity.
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder
				.HasData(GenerateCategories());
		}

		/// <summary>
		/// Method for generating categories.
		/// </summary>
		/// <returns></returns>
		private Category[] GenerateCategories()
		{
			ICollection<Category> categories = new HashSet<Category>();

			Category category;

			category = new Category
			{
				Id = 1,
				Name = "Vehicle",
			};
			categories.Add(category);

			category = new Category
			{
				Id = 2,
				Name = "Bicycle",
			};
			categories.Add(category);

			category = new Category
			{
				Id = 3,
				Name = "Real Estate",
			};
			categories.Add(category);

			return categories.ToArray();
		}
	}
}
