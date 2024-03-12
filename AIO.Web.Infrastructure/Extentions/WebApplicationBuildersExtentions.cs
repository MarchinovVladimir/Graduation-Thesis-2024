﻿using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using static AIOCommon.GeneralAppConstants;

namespace Microsoft.Extensions.DependencyInjection
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
		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
		{
			string connectionString = config.GetConnectionString("DefaultConnection");
			services.AddDbContext<AIODbContext>(options =>
				options.UseSqlServer(connectionString));

			return services;
		}

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
		{
			using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

			IServiceProvider serviceProvider = scopedServices.ServiceProvider;

			UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			Task.Run(async () =>
			{
				if (await roleManager.RoleExistsAsync(AdminRoleName))
				{
					return;
				}

				IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);
				await roleManager.CreateAsync(role);

				ApplicationUser adminUser = await userManager.FindByEmailAsync(email);

				await userManager.AddToRoleAsync(adminUser, AdminRoleName);
			})
			.GetAwaiter()
			.GetResult();

			return app;
		}
	}
}
