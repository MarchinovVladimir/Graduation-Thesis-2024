using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Category;

namespace AIO.Data.Models
{
    /// <summary>
    /// Category of the product.
    /// </summary>
    [Comment("The category of the product.")]
    public class Category
    {
        /// <summary>
        /// Category's unique identifier.
        /// </summary>
        [Comment("Category's unique identifier.")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        [Comment("Category name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Products collection with same category.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();    
    }
}
