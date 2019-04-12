using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Tests.TestData
{
	[SqlConstantContainer(ViewName = "enum_constants_named")]
	internal enum EnumConstantsNamed
	{
		[SqlConstant] Value0 = 0,
		[SqlConstant] Value1 = 1,
		[SqlConstant] ValueTwo = 2,
		[SqlConstant(ColumnName = "value_3")] ValueThree = 3,
	}
}