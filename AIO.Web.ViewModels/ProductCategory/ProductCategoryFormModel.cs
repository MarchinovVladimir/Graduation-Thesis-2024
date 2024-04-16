using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Category;
using static AIOCommon.ErrorMessageConstants.Category;

namespace AIO.Web.ViewModels.ProductCategory
{
	public class ProductCategoryFormModel
	{
		[Required]
		[Display(Name = "Category Name")]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength, 
			ErrorMessage = NameLengthErrorMessage)]
		public string Name { get; set; } = null!;
	}
}
