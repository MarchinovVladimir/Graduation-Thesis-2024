using AIO.Views.Product.Enums;
using System.ComponentModel.DataAnnotations;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Web.ViewModels.Product
{
	public class AllProductsQueryModel
	{
        public string? Category { get; set; }

		[Display(Name = "Search by name")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort products by")]
		public ProductSorting ProductSorting { get; set; }

		public int CurrentPage { get; set; } = CurrentPageDefaultValue;

		public int TotalProducts { get; set; }

		[Display(Name = "Products per page")]
		public int ProductsPerPage { get; set; } = ProductsPerPageDefaultValue;

		public IEnumerable<string> Categories { get; set; } = new HashSet<string>();

		public IEnumerable<ProductAllViewModel> Products { get; set; } = new HashSet<ProductAllViewModel>();

    }
}
