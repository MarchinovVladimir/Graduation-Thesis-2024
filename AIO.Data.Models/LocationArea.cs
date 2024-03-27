using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.LocationArea;

namespace AIO.Data.Models
{
	/// <summary>
	/// Location area entity model.
	/// </summary>
	[Comment("The location area where the product is located.")]
	public class LocationArea
	{
		/// <summary>
		/// Location area's unique identifier.
		/// </summary>
		[Comment("The location area's unique identifier.")]
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Location area name.
		/// </summary>
		[Comment("The location area's name.")]
		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		/// <summary>
		/// Location area postal code.
		/// </summary>
		[Comment("The location area's postal code.")]
		[Required]
		[MaxLength(PostCodeMaxLength)]
		public string PostCode { get; set; } = null!;

		/// <summary>
		/// The products that are located in this location area.
		/// </summary>
		public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
	}
}
