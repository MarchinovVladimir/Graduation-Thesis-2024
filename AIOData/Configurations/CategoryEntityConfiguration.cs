using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasData(GenerateCategories());
        }

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
