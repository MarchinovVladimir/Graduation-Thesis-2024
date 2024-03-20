using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AIOCommon.EntityValidationConstants.Product;

namespace AIO.Data.Models
{
    /// <summary>
    /// Product entity class.
    /// </summary>
    [Comment("The product that is for sell.")]
    public class Product
    {
        /// <summary>
        /// Product's entity property Id unique identifier.
        /// </summary>
        [Comment("The product's unique identifier.")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

		/// <summary>
		/// Product's entity property title.
		/// </summary>
		[Comment("The product's title.")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

		/// <summary>
		/// Product's entity property description.
		/// </summary>
		[Comment("The product's description.")]
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Product's entity property image URL.
        /// </summary>
        [Comment("The product's image URL.")]
        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Product's entity property price.
        /// </summary>
        [Comment("The product's price.")]
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Product's entity property start time.
        /// </summary>
        [Comment("The product's start time.")]
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Product's entity property end time.
        /// </summary>
        [Comment("The product's end time.")]
        [Required]
        public DateTime EndTime { get; set; }

		/// <summary>
		/// Product's entity property IsActive (is deleted).   
		/// </summary>
		[Comment("The product's status.")]
        [Required]
        public bool IsActive { get; set; }

		/// <summary>
		/// Product's entity property CategoryId identifier.
		/// </summary>
		[Comment("The product's category identifier.")]
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Product's entity property Category. Navigation property.
        /// </summary>
        [Comment("The product's category.")]
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Product's entity property AgentId identifier.
        /// </summary>
        [Comment("The product's seller identifier.")]
        [Required]
        public Guid AgentId { get; set; }

        /// <summary>
        /// Product's entity property Agent. Navigation property.
        /// </summary>
        [Comment("The product's seller.")]
        [Required]
        [ForeignKey(nameof(AgentId))]
        public virtual Agent Agent { get; set; } = null!;

        /// <summary>
        /// Product's entity property BuyerId identifier.
        /// </summary>
        [Comment("The product's watcher identifier.")]
        public Guid? BuyerId { get; set; }

        /// <summary>
        /// Product's entity property Buyer. Navigation property.
        /// </summary>
        [Comment("The product's buyer.")]
        [ForeignKey(nameof(BuyerId))]
        public virtual ApplicationUser? Buyer { get; set; }
    }
}
