using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIO.Web.ViewModels.Agent
{
	public class AgentInfoOnProductViewModel
	{
		[Required]
		public string Email { get; set; } = null!;

		[Required]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
