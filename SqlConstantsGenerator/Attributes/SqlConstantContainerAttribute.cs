using System;

namespace SqlConstantsGenerator.Attributes
{
	/// <summary> Sql constants container descriptor </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
	public class SqlConstantContainerAttribute : Attribute
	{
		/// <summary> Name for the view </summary>
		public string ViewName { get; set; }

		/// <summary> Comment for the view </summary>
		public string Comment { get; set; }
	}
}
