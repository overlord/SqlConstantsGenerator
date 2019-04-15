using System;

namespace SqlConstantsGenerator.Attributes
{
	/// <summary> Generator options to specify on assembly level </summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	public class SqlConstantsGeneratorOptionsAttribute : Attribute
	{
		/// <summary> Custom destination folder for generated scripts.
		/// Value can contain "$SolutionDir$" or "$ProjectDir$" placeholders to be replaced with actual paths.
		/// </summary>
		public string DestinationFolder { get; set; }

		/// <summary> Prefix sql code </summary>
		public string PrefixSql { get; set; }

		/// <summary> Postfix sql code </summary>
		public string PostfixSql { get; set; }
	}
}
