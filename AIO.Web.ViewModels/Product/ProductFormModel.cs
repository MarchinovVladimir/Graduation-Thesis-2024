using AIO.Web.ViewModels.LocationArea;
using AIO.Web.ViewModels.ProductCategory;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Product;
using static AIOCommon.ErrorMessageConstants.Product;

namespace AIO.Web.ViewModels.Product
{
	/// <summary>
	/// Product form model.
	/// </summary>
	public class ProductFormModel
	{
		/// <summary>
		/// Gets or sets the title of the product form model.
		/// </summary>
		[Required]
		[StringLength(TitleMaxLength,
					  MinimumLength = TitleMinLength,
					  ErrorMessage = TitleLengthErrorMessage)]
		public string Title { get; set; } = null!;

		/// <summary>
		/// Gets or sets the description of the product form model.
		/// </summary>
		[Required]
		[StringLength(DescriptionMaxLength,
					  MinimumLength = DescriptionMinLength,
					  ErrorMessage = DescriptionLengthErrorMessage)]
		public string Description { get; set; } = null!;

		/// <summary>
		/// Gets or sets the image URL of the product form model.
		/// </summary>
		[Required]
		[StringLength(ImageUrlMaxLength,
					  MinimumLength = ImageUrlMinLength,
					  ErrorMessage = URLLengthErrorMessage)]
		[Display(Name = "Image link")]
		[Url(ErrorMessage = "Invalid URL.")]
		public string ImageUrl { get; set; } = null!;

		/// <summary>
		/// Gets or sets the price of the product form model.
		/// </summary>
		[Required]
		[Range(typeof(decimal), PriceMinValue, PriceMaxValue,
			ErrorMessage = PriceRangeErrorMessage)]
		public decimal Price { get; set; }

		/// <summary>
		/// Gets or sets the category ID of the product form model.
		/// </summary>
		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the location ID of the product form model.
		/// </summary>
		[Required]
		[Display(Name = "Location")]
		public int LocationAreaId { get; set; }

		/// <summary>
		/// Gets or sets the categories of the product form model.
		/// </summary>
		[Required]
		public ICollection<ProductCategoryViewModel> Categories { get; set; } =
			new HashSet<ProductCategoryViewModel>();

		/// <summary>
		/// Gets or sets the location areas of the product form model.
		/// </summary>
		[Required]
		public ICollection<LocationAreaViewModel> LocationAreas { get; set; } =
			new HashSet<LocationAreaViewModel>();
	}
}
