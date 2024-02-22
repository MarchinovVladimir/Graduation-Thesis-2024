using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Agent;

namespace AIO.Web.ViewModels.Agent
{
	public class BecomeAgentFormModel
	{
		[Required]
		[Phone]
		[MaxLength(PhoneNumberMaxLength)]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
