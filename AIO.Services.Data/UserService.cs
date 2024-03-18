﻿using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	public class UserService : IUserService
	{
		private readonly AIODbContext dbContext;

		public UserService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<string> GetFullNameByEmailAsync(string email)
		{
			ApplicationUser? user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

			if (user == null)
			{
				return string.Empty;
			}

			return $"{user.FirstName} {user.LastName}";
		}

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
				Agent? agent = await this.dbContext.Agents
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
