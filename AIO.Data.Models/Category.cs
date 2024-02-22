using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Category;

namespace AIO.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();    
    }
}
