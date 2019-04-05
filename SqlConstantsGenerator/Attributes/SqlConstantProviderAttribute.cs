using System;

namespace SqlConstantsGenerator.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
	public class SqlConstantProviderAttribute : Attribute
	{
		public string ViewName { get; set; }
	}
}
