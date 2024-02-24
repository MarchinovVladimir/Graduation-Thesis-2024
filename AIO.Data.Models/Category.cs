using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Category;

namespace AIO.Data.Models
{
    public class Category
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Category name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Products collection with same category")]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();    
    }
}
