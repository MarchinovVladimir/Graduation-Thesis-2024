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
        [Comment("The date when the product listing is created")]
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Product's entity property end time.
        /// </summary>
        [Comment("The date when the product listing expires")]
        [Required]
        public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// Product's entity property IsActive. Shows if the listing is active.   
		/// </summary>
		[Comment("Is product listing active?")]
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Product's entity property IsSold. Shows if the product is sold.
        /// </summary>
        [Comment("Is the product sold?")]
        [Required]
        public bool IsSold { get; set; }    

		/// <summary>
		/// Product's entity property CategoryId identifier.
		/// </summary>
		[Comment("The product's category identifier.")]
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Product's entity property Category. Navigation property.
        /// </summary>
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Product's entity property LocationAreaId identifier.
        /// </summary>
        [Comment("The location area where the product is located.")]
        [Required]
        public int LocationAreaId { get; set; }

        /// <summary>
        /// Product's entity property LocationArea. Navigation property.
        /// </summary>
        [Required]
        [ForeignKey(nameof(LocationAreaId))]
        public LocationArea LocationArea { get; set; } = null!;

        /// <summary>
        /// Product's entity property AgentId identifier.
        /// </summary>
        [Comment("The product's seller identifier.")]
        [Required]
        public Guid SellerId { get; set; }

        /// <summary>
        /// Product's entity property Agent. Navigation property.
        /// </summary>
        [Required]
        [ForeignKey(nameof(SellerId))]
        public virtual Seller Seller { get; set; } = null!;

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
