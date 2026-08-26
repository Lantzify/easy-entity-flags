using EasyEntityFlags.Models;

namespace EasyEntityFlags.Extensions
{
	public static class EasyEntityFlagExtensions
	{
		public static string GetFlagName(this EasyEntityFlag flag)
		{
			return string.Format("EasyEntityFlag_{0}_{1}", flag.PropertyAlias, flag.Label);
		}
	}
}
