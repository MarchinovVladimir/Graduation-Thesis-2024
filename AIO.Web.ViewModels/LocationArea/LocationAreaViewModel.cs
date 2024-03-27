using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.LocatiomArea
{
	/// <summary>
	/// Location area view model.
	/// </summary>
	public class LocationAreaViewModel
	{
		/// <summary>
		/// Id property of LocationAreaViewModel.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name property of LocationAreaViewModel.
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// PostCode property of LocationAreaViewModel.
		/// </summary>
		public string PostCode { get; set; } = null!;
	}
}
