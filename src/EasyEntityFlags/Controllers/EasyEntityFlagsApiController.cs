using System.Linq;
using Asp.Versioning;
using EasyEntityFlags.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyEntityFlags.Extensions;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Api.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Umbraco.Cms.Web.Common.Authorization;

namespace EasyEntityFlags.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[MapToApi("easyEntityFlags")]
	[ApiExplorerSettings(GroupName = "easyEntityFlags")]
	[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
	public class EasyEntityFlagsApiController : Controller
	{
		private readonly IOptions<EasyEntityFlagsSettings> _easyEntityFlagsSettings;

		public EasyEntityFlagsApiController(IOptions<EasyEntityFlagsSettings> easyEntityFlagsSettings)
		{
			_easyEntityFlagsSettings = easyEntityFlagsSettings;
		}

		[HttpGet("get-entity-flags")]
		public IEnumerable<EasyEntityFlagModel> GetEntityFlags()
		{
			var entityFlagSettings = _easyEntityFlagsSettings.Value.EntityFlags;

			if (entityFlagSettings != null && entityFlagSettings.Any())
			{
				var entityFlagList = entityFlagSettings.Select(entityFlag =>
				{
					var model = new EasyEntityFlagModel
					{
						PropertyAlias = entityFlag.PropertyAlias,
						Icon = entityFlag.Icon,
						IconColorAlias = entityFlag.IconColorAlias,
						Label = entityFlag.Label,
						ForEntityTypes = entityFlag.ForEntityTypes,
						Weight = entityFlag.Weight,
						Name = string.Format("EasyEntityFlag {0} {1}", entityFlag.PropertyAlias, entityFlag.Label),
						FlagName = entityFlag.GetFlagName()
					};

					model.Alias = model.Name.Replace(" ", ".");

					return model;
				}).ToList();

				return entityFlagList;
			}
			return Enumerable.Empty<EasyEntityFlagModel>();
		}
	}
}
