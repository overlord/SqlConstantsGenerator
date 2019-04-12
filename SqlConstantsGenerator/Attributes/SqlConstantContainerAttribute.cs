using System;

namespace SqlConstantsGenerator.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
	public class SqlConstantContainerAttribute : Attribute
	{
		public string ViewName { get; set; }
	}
}
