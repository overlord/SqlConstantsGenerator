using System;

namespace SqlConstantsGenerator.Attributes
{
	[AttributeUsage(AttributeTargets.Assembly)]
	public class SqlConstantsGeneratorOptionsAttribute : Attribute
	{
		public string PrefixSql { get; set; }
		public string PostfixSql { get; set; }
	}
}
