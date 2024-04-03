using AIO.Data;
using AIO.Data.Models;

namespace AIO.Services.Tests
{
	public static class DatabaseSeeder
	{
		public static ApplicationUser? SellerUser;
		public static ApplicationUser? User;
		public static Seller? Seller;

		public static void SeedDatabase(AIODbContext dbContext)
		{
			Category[] categories = new Category[]
			{
				new Category()
				{
					Name = "Vehicle"
				},
				new Category()
				{
					Name = "Bicycle"
				},
				new Category()
				{
					Name = "Real Estate"
				}
			};

			dbContext.Categories.AddRange(categories);

			LocationArea[] locationAreas = new LocationArea[]
			{
				new LocationArea()
				{
					Name = "Sofia",
					PostCode = "1000"
				},
				new LocationArea()
				{
					Name = "Plovdiv",
					PostCode = "4000"
				},
				new LocationArea()
				{
					Name = "Varna",
					PostCode = "9000"
				},
				new LocationArea()
				{
					Name = "Burgas",
					PostCode = "8000"
				}
			};

			dbContext.LocationAreas.AddRange(locationAreas);

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


			Product productHouse = new Product()
			{
				Title = "House",
				Description = "A house for sale",
				ImageUrl = "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc ()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
				Price = 100000m,
				CategoryId = 3,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				IsActive = true,
				CreatedOn = DateTime.Parse("2024-03-27 14:00:07.6830277"),
				ExpirationDate = DateTime.Parse("2024-04-03 14:00:07.6830277"),
				IsSold = false,
				LocationAreaId = 3
			};

			Product productCar = new Product()
			{
				Title = "Car",
				Description = "A car for sale",
				ImageUrl = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?   q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8  fDA%3D",
				Price = 10000m,
				CategoryId = 1,
				SellerId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
				IsActive = false,
				CreatedOn = DateTime.Parse("2024-03-21 17:23:43.6400000"),
				ExpirationDate = DateTime.Parse("2024-03-28 17:23:43.6400000"),
				IsSold = true,
				LocationAreaId = 1,
			};



			dbContext.Products.Add(productHouse);
			dbContext.Products.Add(productCar);

			Seller.ProductsForSell.Add(productHouse);

			dbContext.SaveChanges();

		}
	}
}
