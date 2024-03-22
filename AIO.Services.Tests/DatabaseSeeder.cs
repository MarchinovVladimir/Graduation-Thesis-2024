using AIO.Data;
using AIO.Data.Models;
using AIO.Web.ViewModels.ProductCategory;

namespace AIO.Services.Tests
{
	public static class DatabaseSeeder
	{
		public static ApplicationUser? AgentUser;
		public static ApplicationUser? User;
		public static Seller? Seller;

		public static void SeedDatabase(AIODbContext dbContext)
		{
			AgentUser = new ApplicationUser()
			{
				UserName = "Pesho",
				NormalizedUserName = "PESHO",
				Email = "pesho@agents.com",
				NormalizedEmail = "PESHO@AGENTS.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Pesho",
				LastName = "Petrov",
				
			};
			User = new ApplicationUser()
			{
				UserName = "Gosho",
				NormalizedUserName = "GOSHO",
				Email = "gosho@renters.com",
				NormalizedEmail = "GOSHO@RENTERS.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
				SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
				TwoFactorEnabled = false,
				FirstName = "Gosho",
				LastName = "Goshov"
			};
			Seller = new Seller()
			{
				PhoneNumber = "+359888888888",
				User = AgentUser,
				//ProductsForSell = new List<Product>()
				//{
				//	new Product()
				//	{
				//		Id = Guid.Parse("7be0b259-d4d0-46e8-b15d-686809748e4b"),
				//		Name = "Product1",
				//		Price = 100,
				//		Quantity = 10,
				//	}
				//}
			};

			dbContext.Users.Add(AgentUser);
			dbContext.Users.Add(User);
			dbContext.Sellers.Add(Seller);

			dbContext.SaveChanges();
		}
	}
}
