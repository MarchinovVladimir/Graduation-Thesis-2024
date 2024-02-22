using AIO.Web.ViewModels.Home;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts();
	}
}
