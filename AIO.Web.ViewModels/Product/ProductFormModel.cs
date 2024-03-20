using AIO.Web.ViewModels.ProductCategory;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Product;

namespace AIO.Web.ViewModels.Product
{
	public class ProductFormModel
	{ 
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(ImageUrlMaxLength)]
		[Display(Name = "Image link")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		// But validation here
		public decimal Price { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Required]
		public ICollection<ProductCategoryViewModel> Categories { get; set; } = new HashSet<ProductCategoryViewModel>();
	}
}
