using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Agent;

namespace AIO.Web.ViewModels.Agent
{
	/// <summary>
	/// View model for becoming a seller.
	/// </summary>
	public class BecomeSellerFormModel
	{
		/// <summary>
		/// Phone number property of the BecomeSellerFormModel.
		/// </summary>
		[Required]
		[Phone]
		[MaxLength(PhoneNumberMaxLength)]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
