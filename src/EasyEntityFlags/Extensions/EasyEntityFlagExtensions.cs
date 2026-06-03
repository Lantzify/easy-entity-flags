using EasyEntityFlags.Models;

namespace EasyEntityFlags.Extensions
{
	public static class EasyEntityFlagExtensions
	{
		public static string GetFlagName(this EasyEntityFlag flag)
		{
			return string.Format("EasyEntityFlag_{0}", flag.PropertyAlias);
		}
	}
}
