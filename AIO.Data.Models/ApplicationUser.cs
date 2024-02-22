using Microsoft.AspNetCore.Identity;

namespace AIO.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Product> ProductsBought { get; set; } = new HashSet<Product>();
    }
}
