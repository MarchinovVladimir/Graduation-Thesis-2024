using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	/// <summary>
	/// User service for handling user-related operations.
	/// </summary>
	public class UserService : IUserService
	{
		private readonly AIODbContext dbContext;

		public UserService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Get the full name of a user by email.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public async Task<string> GetFullNameByEmailAsync(string email)
		{
			ApplicationUser? user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

			if (user == null)
			{
				return string.Empty;
			}

			return $"{user.FirstName} {user.LastName}";
		}

		/// <summary>
		/// Get the full name of a user by id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<string> GetFullNameByIdAsync(string userId)
		{
			ApplicationUser? user = await this.dbContext
				.Users
				.FirstOrDefaultAsync(x => x.Id.ToString() == userId);

			if (user == null)
			{
				return String.Empty;
			}

			return $"{user.FirstName} {user.LastName}";
		}

		/// <summary>
		/// Get all users.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<UserViewModel>> AllAsync()
		{
			List<UserViewModel> allUsers = await this.dbContext
				.Users
				.Select(u => new UserViewModel()
				{
					Id = u.Id.ToString(),
					Email = u.Email,
					FullName = $"{u.FirstName} {u.LastName}"
				}).ToListAsync();

			foreach (var user in allUsers)
			{
				Seller? agent = await this.dbContext.Sellers
					.FirstOrDefaultAsync(a => a.UserId.ToString() == user.Id);

				if (agent != null)
				{
					user.PhoneNumber = agent.PhoneNumber;
				}
				else
				{
					user.PhoneNumber = string.Empty;
				}
			}

			return allUsers;
		}
	}
}
