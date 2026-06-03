namespace EasyEntityFlags.Models
{
	public enum FlagCondition
	{
		HasValue,
		HasNoValue,
		IsTrue,
		IsFalse
	}

	public class EasyEntityFlag
	{
		public string PropertyAlias { get; set; }
		public string Icon { get; set; }
		public string? IconColorAlias { get; set; }
		public string Label { get; set; }
		public string[]? ForEntityTypes { get; set; } = new string[] { "document" };
		public FlagCondition Condition { get; set; } = FlagCondition.IsTrue;
	}
}
