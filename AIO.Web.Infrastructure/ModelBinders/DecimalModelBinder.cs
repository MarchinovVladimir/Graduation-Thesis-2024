using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace AIO.Web.Infrastructure.ModelBinders
{
	public class DecimalModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			ValueProviderResult valueResult = bindingContext
												.ValueProvider
												.GetValue(bindingContext.ModelName);

			if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
			{
				decimal result = 0M;
				bool success = false;

				try
				{
					string stringValue = valueResult.FirstValue.Trim();
					stringValue = stringValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					stringValue = stringValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

					result = Convert.ToDecimal(stringValue, CultureInfo.CurrentCulture);
					success = true;
				}
				catch (FormatException fe)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
				}

				if (success)
				{
					bindingContext.Result = ModelBindingResult.Success(result);
				}
			}

			return Task.CompletedTask;
		}
	}
}
