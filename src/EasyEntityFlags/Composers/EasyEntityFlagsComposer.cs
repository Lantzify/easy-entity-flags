using Umbraco.Extensions;
using EasyEntityFlags.Models;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyEntityFlags.Composers
{
	public class EasyEntityFlagsComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.FlagProviders().Append<EasyEntityFlagsProvider>();

			builder.Services.AddOptions<EasyEntityFlagsSettings>()
				.Bind(builder.Config.GetSection(EasyEntityFlagsSettings.EasyEntityFlags));

			builder.AddBackOfficeOpenApiDocument("EasyEntityFlags", document =>
			{
				document.WithTitle("Easy Entity Flags")
						.WithBackOfficeAuthentication()
						.ConfigureOpenApiOptions(options =>
						{
							options.AddDocumentTransformer((doc, _, _) =>
							{
								doc.Info.Version = "Latest";
								return Task.CompletedTask;
							});
						});
			});
		}
	}
}
