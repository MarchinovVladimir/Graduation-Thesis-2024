using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AIOCommon.EntityValidationConstants.Seller;
namespace AIO.Data.Models
{
    /// <summary>
    /// Seller entity. Represents the user who sells products.
    /// </summary>
    [Comment("Agent entity. Represents the user who sells products.")]
    public class Seller
    {
		/// <summary>
		/// Seller's entity property Id unique identifier.
		/// </summary>
		[Comment("Agent identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

		/// <summary>
		/// Seller's entity property PhoneNumber.
		/// </summary>
		[Comment("Agent phone number")]
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

		/// <summary>
		/// Seller's entity property UserId.
		/// </summary>
		[Comment("User identifier")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigational property of ApplicationUser
        /// </summary>
        [Comment("Navigational property of ApplicationUser")]
        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Products collection with same seller
        /// </summary>
        [Comment("Products collection with same seller")]
        public virtual ICollection<Product> ProductsForSell { get; set; } = new HashSet<Product>();
    }
}