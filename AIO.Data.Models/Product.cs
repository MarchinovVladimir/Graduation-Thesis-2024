using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AIOCommon.EntityValidationConstants.Product;

namespace AIO.Data.Models
{
    [Comment("The product that is being auctioned.")]
    public class Product
    {
        [Comment("The product's unique identifier.")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("The product's title.")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Comment("The product's description.")]
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("The product's image URL.")]
        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Comment("The product's opening bid.")]
        [Required]
        public decimal OpeningBid { get; set; }

        [Comment("The product's current bid.")]
        [Required]
        public decimal CurrentBid { get; set; }

        [Comment("The product's start time.")]
        [Required]
        public DateTime StartTime { get; set; }

        [Comment("The product's end time.")]
        [Required]
        public DateTime EndTime { get; set; }

        [Comment("The product's category identifier.")]
        [Required]
        public int CategoryId { get; set; }

        [Comment("The product's category.")]
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("The product's auction status.")]
        [Required]
        public bool IsAuctionClosed { get; set; }

        [Comment("The product's agent identifier.")]
        [Required]
        public Guid AgentId { get; set; }

        [Comment("The product's agent.")]
        [Required]
        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;

        [Comment("The product's buyer identifier.")]
        public Guid? BuyerId { get; set; }

        [Comment("The product's buyer.")]
        [ForeignKey(nameof(BuyerId))]
        public virtual ApplicationUser? Buyer { get; set; }
    }
}
