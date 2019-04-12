﻿using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Tests.TestData
{
	[SqlConstantContainer]
	internal enum EnumConstants
	{
		[SqlConstant] Value0 = 0,
		[SqlConstant] Value1 = 1,
		[SqlConstant] ValueTwo = 2,
		[SqlConstant(ColumnName = "value_3")] ValueThree = 3,
	}
}
