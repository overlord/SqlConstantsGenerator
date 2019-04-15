using System;

namespace SqlConstantsGenerator.Attributes
{
	/// <summary> Sql constant descriptor </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class SqlConstantAttribute : Attribute
	{
		/// <summary> Column name for the constant </summary>
		public string ColumnName { get; set; }

		/// <summary> Comment for the constant value </summary>
		public string Comment { get; set; }
	}
}
