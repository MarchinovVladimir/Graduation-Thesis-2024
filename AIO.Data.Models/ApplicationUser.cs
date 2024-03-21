using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.User;

namespace AIO.Data.Models
{
	/// <summary>
	/// This is custom udser class that inherits from IdentityUser and adds additional properties.
	/// </summary>
	[Comment("The application user entity.")]
	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// Application user's first name.
		/// </summary>
		[Comment("Application user's first name.")]
		[Required]
		[MaxLength(FirstNameMaxLength)]
		public string FirstName { get; set; } = null!;

		/// <summary>
		/// Application user's last name.
		/// </summary>
		[Comment("Application user's last name.")]
		[Required]
		[MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;

		/// <summary>
		/// Application user's watched products.
		/// </summary>
		[Comment("Application user's watched products.")]
		public virtual ICollection<Product> ProductsWatched { get; set; } = new HashSet<Product>();
	}
}
