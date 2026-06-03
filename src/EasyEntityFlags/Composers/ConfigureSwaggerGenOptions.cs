using Microsoft.OpenApi;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

namespace EasyEntityFlags.Composers
{
	internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
	{
		public void Configure(SwaggerGenOptions options)
		{
			options.SwaggerDoc(
			  "easyEntityFlags",
			  new OpenApiInfo
			  {
				  Title = "Easy Entity Flags Api",
				  Version = "Latest"
			  });

			options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
		}
	}
}
