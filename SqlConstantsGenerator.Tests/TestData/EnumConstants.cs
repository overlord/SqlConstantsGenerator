using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Tests.TestData
{
	[SqlConstantProvider]
	public enum EnumConstants
	{
		[SqlConstant] Value0 = 0,
		[SqlConstant] Value1 = 1,
		[SqlConstant] ValueTwo = 2,
		[SqlConstant(ColumnName = "value_3")] ValueThree = 3,
	}

	[SqlConstantProvider(ViewName = "enum_constants_named")]
	public enum EnumConstantsNamed
	{
		[SqlConstant] Value0 = 0,
		[SqlConstant] Value1 = 1,
		[SqlConstant] ValueTwo = 2,
		[SqlConstant(ColumnName = "value_3")] ValueThree = 3,
	}
}
