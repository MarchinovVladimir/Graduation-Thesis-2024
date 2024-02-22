using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AIOCommon.EntityValidationConstants.Product;

namespace AIO.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal OpeningBid { get; set; }

        [Required]
        public decimal CurrentBid { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public bool IsAuctionClosed { get; set; }

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;

        public Guid? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public virtual ApplicationUser? Buyer { get; set; }
    }
}
