using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AIOCommon.EntityValidationConstants.Seller;
namespace AIO.Data.Models
{
    [Comment("Agent entity. Represents the user who sells products.")]
    public class Agent
    {
        
        [Comment("Agent identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Agent phone number")]
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Bidder identifier")]
        public Guid UserId { get; set; }

        [Comment("Bidder user")]
        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Comment("Products collection with same seller")]
        public virtual ICollection<Product> ProductsForSell { get; set; } = new HashSet<Product>();
    }
}