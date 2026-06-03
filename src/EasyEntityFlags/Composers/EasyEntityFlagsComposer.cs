using Umbraco.Extensions;
using EasyEntityFlags.Models;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyEntityFlags.Composers
{
	public class EasyEntityFlagsComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

			builder.FlagProviders().Append<EasyEntityFlagsProvider>();

			builder.Services.AddOptions<EasyEntityFlagsSettings>()
				.Bind(builder.Config.GetSection(EasyEntityFlagsSettings.EasyEntityFlags));
		}
	}
}
