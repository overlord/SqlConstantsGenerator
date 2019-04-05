using System;

namespace SqlConstantsGenerator.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class SqlConstantAttribute : Attribute
	{
		public string ColumnName { get; set; }
		public string Comment { get; set; }
	}
}
