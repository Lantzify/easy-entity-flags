using System.Collections.Generic;

namespace EasyEntityFlags.Models
{
	public class EasyEntityFlagsSettings
	{
		public const string EasyEntityFlags = "EasyEntityFlags";
		public IEnumerable<EasyEntityFlag>? EntityFlags { get; set; }
	}
}
