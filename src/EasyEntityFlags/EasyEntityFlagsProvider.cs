using System;
using System.Linq;
using Umbraco.Extensions;
using Umbraco.Cms.Core.Web;
using EasyEntityFlags.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using EasyEntityFlags.Extensions;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Api.Management.ViewModels;
using Umbraco.Cms.Api.Management.Services.Flags;
using Umbraco.Cms.Api.Management.ViewModels.Tree;
using Umbraco.Cms.Api.Management.ViewModels.Document.Item;
using Umbraco.Cms.Api.Management.ViewModels.Document.Collection;

namespace EasyEntityFlags
{
	public class EasyEntityFlagsProvider : IFlagProvider
	{

		private readonly IUmbracoContextFactory _umbracoContextFactory;
		private readonly IOptions<EasyEntityFlagsSettings> _easyEntityFlagsSettings;

		public EasyEntityFlagsProvider(IUmbracoContextFactory umbracoContextFactory, 
			IOptions<EasyEntityFlagsSettings> easyEntityFlagsSettings)
		{
			_umbracoContextFactory = umbracoContextFactory;
			_easyEntityFlagsSettings = easyEntityFlagsSettings;
		}

		public bool CanProvideFlags<TItem>()
			where TItem : IHasFlags =>
			typeof(TItem) == typeof(DocumentTreeItemResponseModel) ||
			typeof(TItem) == typeof(DocumentCollectionResponseModel) ||
				typeof(TItem) == typeof(DocumentItemResponseModel);

		public Task PopulateFlagsAsync<TItem>(IEnumerable<TItem> items) where TItem : IHasFlags
		{
			using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();

			foreach (TItem item in items)
			{
				Guid itemId = Guid.Empty;

				switch (item)
				{
					case DocumentTreeItemResponseModel treeItem:
						itemId = treeItem.Id;
						break;
					case DocumentCollectionResponseModel collectionItem:
						itemId = collectionItem.Id;
						break;

					case DocumentItemResponseModel documentItem:
						itemId = documentItem.Id;
						break;
				}

				if (itemId == Guid.Empty)
					return Task.CompletedTask; 

				var content = umbracoContextReference.UmbracoContext.Content?.GetById(itemId);
				if (content == null)
					return Task.CompletedTask;

				var entityFlagSettings = _easyEntityFlagsSettings.Value.EntityFlags;
				if (entityFlagSettings != null && entityFlagSettings.Any())
				{
					foreach (var flag in entityFlagSettings)
					{
						bool shouldAddFlag = flag.Condition switch
						{
							FlagCondition.HasValue   => content.HasValue(flag.PropertyAlias),
							FlagCondition.HasNoValue => !content.HasValue(flag.PropertyAlias),
							FlagCondition.IsTrue     => content.Value<bool>(flag.PropertyAlias),
							FlagCondition.IsFalse    => !content.Value<bool>(flag.PropertyAlias),
							_                        => content.Value<bool>(flag.PropertyAlias)
						};

						if (shouldAddFlag)
							item.AddFlag(flag.GetFlagName());
					}
				}				
			}

			return Task.CompletedTask;
		}
	}
}
