using CommandLine;
using System.Runtime.InteropServices;

namespace EasyEntityFlags.SchemaGenerator
{
	internal class Options
	{
		[Option('o', "outputFile", Required = false,
			HelpText = "",
			Default = "..\\..\\..\\..\\EasyEntityFlags\\appsettings-schema.easy-entity-flags.json")]
		public string OutputFile { get; set; } = "..\\..\\..\\..\\EasyEntityFlags\\appsettings-schema.easy-entity-flags.json";
	}
}