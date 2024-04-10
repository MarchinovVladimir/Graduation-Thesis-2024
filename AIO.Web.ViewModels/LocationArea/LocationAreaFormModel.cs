using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.LocationArea;
using static AIOCommon.ErrorMessageConstants.LocationArea;

namespace AIO.Areas.Admin.ViewModels
{
	public class LocationAreaFormModel
	{
		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameLengthErrorMessage)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(PostCodeMaxLength, MinimumLength = PostCodeMinLength, ErrorMessage = PostCodeLengthErrorMessage)]
		public string PostCode { get; set; } = null!;
	}
}
