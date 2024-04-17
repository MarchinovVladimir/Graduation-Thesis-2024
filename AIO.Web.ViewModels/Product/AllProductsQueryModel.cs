using AIO.Views.Product.Enums;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Web.ViewModels.Product
{
	/// <summary>
	/// All products query model.
	/// </summary>
	public class AllProductsQueryModel
	{
		/// <summary>
		/// Category property of the AllProductsQueryModel.
		/// </summary>
		public string? Category { get; set; }

		/// <summary>
		/// LocationArea property of the AllProductsQueryModel.
		/// </summary>
		[Display(Name = "Location")]
		public string? LocationArea { get; set; }

		/// <summary>
		/// ProductsPerPage property of the AllProductsQueryModel.
		/// </summary>
		[Display(Name = "Products per page")]
		public int ProductsPerPage { get; set; } = ProductsPerPageDefaultValue;

		/// <summary>
		/// SearchString property of the AllProductsQueryModel.
		/// </summary>
		[Display(Name = "Search by name")]
		public string? SearchString { get; set; }

		/// <summary>
		/// ProductSorting property of the AllProductsQueryModel.
		/// </summary>
		[Display(Name = "Sort products by")]
		public ProductSorting ProductSorting { get; set; }

		/// <summary>
		/// CurrentPage property of the AllProductsQueryModel.
		/// </summary>
		public int CurrentPage { get; set; } = CurrentPageDefaultValue;

		/// <summary>
		/// TotalProducts property of the AllProductsQueryModel.
		/// </summary>
		public int TotalProducts { get; set; }

		/// <summary>
		/// Categories property of the AllProductsQueryModel.
		/// </summary>
		public IEnumerable<string> Categories { get; set; } = new HashSet<string>();

		/// <summary>
		/// LocationAreas property of the AllProductsQueryModel.
		/// </summary>
		public IEnumerable<string> LocationAreas { get; set; } = new HashSet<string>();

		/// <summary>
		/// Products property of the AllProductsQueryModel.
		/// </summary>
		public IEnumerable<ProductAllViewModel> Products { get; set; } =
			new HashSet<ProductAllViewModel>();

	}
}
