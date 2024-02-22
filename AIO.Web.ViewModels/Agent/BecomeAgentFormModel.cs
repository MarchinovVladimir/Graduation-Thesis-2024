using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Agent
{
	public class BecomeAgentFormModel
	{
		[Required]
		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}
