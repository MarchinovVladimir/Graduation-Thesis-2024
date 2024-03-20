using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.User;

namespace AIO.Data.Models
{
	/// <summary>
	/// The application user entity.
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			Id = Guid.NewGuid();
		}


		[Required]
		[MaxLength(FirstNameMaxLength)]
		public string FirstName { get; set; } = null!;

		[Required]
		[MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;
		
		public virtual ICollection<Product> ProductsWatched { get; set; } = new HashSet<Product>();
	}
}
