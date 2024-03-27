using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
	public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder
				.Property(p => p.CreatedOn)
				.HasDefaultValueSql("GETDATE()");

			builder.Property(p => p.ExpirationDate)
				   .HasComputedColumnSql("DATEADD(DAY, 7, CreatedOn)");

			builder
				.Property(p => p.IsActive)
				.HasDefaultValue(true);


			builder
				.Property(p => p.IsSold)
				.HasDefaultValue(false);

			builder
				.Property(p => p.LocationAreaId)
				.HasDefaultValue(1);

			builder
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.LocationArea)
				.WithMany(l => l.Products)
				.HasForeignKey(p => p.LocationAreaId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.Seller)
				.WithMany(a => a.ProductsForSell)
				.HasForeignKey(p => p.SellerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.Buyer)
				.WithMany(b => b.ProductsWatched)
				.HasForeignKey(p => p.BuyerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			builder
				.HasData(GenerateProducts());
		}

		private Product[] GenerateProducts()
		{
			ICollection<Product> products = new HashSet<Product>();

			Product product;

			product = new Product
			{
				Title = "Car",
				Description = "A car for sale",
				CategoryId = 1,
				ImageUrl = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D",
				Price = 10000,
				LocationAreaId = 1,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),
				CreatedOn = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow.AddDays(7),
			};
			products.Add(product);

			product = new Product
			{
				Title = "Bike",
				Description = "A bike for sale",
				CategoryId = 2,
				ImageUrl = "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
				Price = 100,
				LocationAreaId = 2,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),
				CreatedOn = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow.AddDays(7),
			};
			products.Add(product);

			product = new Product
			{
				Title = "House",
				Description = "A house for sale",
				CategoryId = 3,
				ImageUrl = "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
				Price = 100000,
				LocationAreaId = 3,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				BuyerId = null,
				CreatedOn = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow.AddDays(7),
			};
			products.Add(product);

			product = new Product
			{
				Title = "Phone",
				Description = "A phone for sale",
				CategoryId = 4,
				ImageUrl = "https://t3.ftcdn.net/jpg/03/20/87/00/360_F_320870096_CfJYzmZN5kUhFFkzkv17CKppGjqRBqSE.jpg",
				Price = 1000,
				LocationAreaId = 4,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),
				CreatedOn = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow.AddDays(7),
			};

			return products.ToArray();
		}
	}
}
