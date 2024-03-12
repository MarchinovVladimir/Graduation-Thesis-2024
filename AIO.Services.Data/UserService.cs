using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    }
}
