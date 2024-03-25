using AIO.Web.ViewModels.ProductCategory;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Product;
using static AIOCommon.ErrorMessageConstants.Product;

namespace AIO.Web.ViewModels.Product
{
	public class ProductFormModel
	{ 
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = TitleLengthErrorMessage)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthErrorMessage)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength, ErrorMessage = URLLengthErrorMessage)]
		[Display(Name = "Image link")]
		[Url(ErrorMessage = "Invalid URL.")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = PriceRangeErrorMessage)]
		public decimal Price { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Required]
		public ICollection<ProductCategoryViewModel> Categories { get; set; } = new HashSet<ProductCategoryViewModel>();
	}
}
