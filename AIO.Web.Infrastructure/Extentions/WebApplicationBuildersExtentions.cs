using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AIO.Web.Infrastructure.Extentions
{
	public static class WebApplicationBuildersExtentions
	{
		/// <summary>
		/// This method registers all services with their interfaces and implementations of given assambly
		/// The assembly is taken from the provided service interface or implementation
		/// </summary>
		/// <param name="serviceType">Type of random service implementation</param>
		/// <exception cref="InvalidOperationException"></exception>

		public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
			if (serviceAssembly == null)
			{
				throw new InvalidOperationException("Invalid service type provided!");
			}
			Type[] implementationTypes = serviceAssembly
				.GetTypes().
				Where(t => t.Name.EndsWith("Service") &&
					  !t.IsInterface).ToArray();
			foreach (Type implementationType in implementationTypes)
			{
				Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
				if (interfaceType == null)
				{
					throw new InvalidOperationException($"No interface found for {implementationType.Name}!");
				}

				services.AddScoped(interfaceType, implementationType);
			}
			services.AddScoped<IProductService, ProductService>();	
		}
	}
}
