using AIO.Data;
using AIO.Data.Models;

namespace AIO.Services.Tests
{
	public static class DatabaseSeeder
	{
		public static ApplicationUser? SellerUser;
		public static ApplicationUser? User;
		public static Seller? Seller;
		public static Product? Product;

		public static void SeedDatabase(AIODbContext dbContext)
		{						

			SellerUser = new ApplicationUser()
			{
				UserName = "Pesho",
				NormalizedUserName = "PESHO",
				Email = "pesho@seller.com",
				NormalizedEmail = "PESHO@SELLER.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEC2LCFmeRRB/M5r6qyt2eMSpD4QPq4ZzRWoyaVywfOl4lA+eqXtrZp9xoNZIkm/Lxg==",
				SecurityStamp = "RHRFZYG3FDEPWQYIFRD7V7E77CWJN6GB",
				ConcurrencyStamp = "83c2f80c-1bd1-4d97-a35f-df62c45f15e6",
				TwoFactorEnabled = false,
				FirstName = "Pesho",
				LastName = "Petrov",

			};

			dbContext.Users.Add(SellerUser);

			User = new ApplicationUser()
			{
				UserName = "Gosho",
				NormalizedUserName = "GOSHO",
				Email = "gosho@buyer.com",
				NormalizedEmail = "GOSHO@BUYER.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
				SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
				TwoFactorEnabled = false,
				FirstName = "Gosho",
				LastName = "Goshov"
			};

			dbContext.Users.Add(User);

			Seller = new Seller()
			{
				PhoneNumber = "+359889001299",
				User = SellerUser,				
			};

			dbContext.Sellers.Add(Seller);

			Product = new Product()
			{
				Title = "TestProduct",
				Description = "TestProductDescription",
				ImageUrl = "TestProductImageUrl",
				Price = 100,
				CreatedOn = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow.AddDays(7),
				IsActive = true,
				IsSold = false,
				CategoryId = 1,
				LocationAreaId = 1,
				SellerId = Seller.Id,
				
			};

			dbContext.Products.Add(Product);

			dbContext.SaveChanges();
		}
	}
}
