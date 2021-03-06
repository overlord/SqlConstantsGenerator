﻿using System;
using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Tests.TestData
{
	[SqlConstantContainer]
	internal static class StaticClassConstants
	{
		[SqlConstant] public static readonly string StringNull = null;
		[SqlConstant] public static readonly string StringEmpty = "";
		[SqlConstant] public static readonly string String12345 = "12345";

		[SqlConstant(ColumnName = "string_ololo")]
		public static readonly string StringOlolo = "string_ololo_value";

		[SqlConstant(ColumnName = "string_ololo_comment", Comment = "Hello! World!")]
		public static readonly string StringOloloComment = "string_ololo_comment_value";

		[SqlConstant] public static readonly int Int0 = 0;
		[SqlConstant] public static readonly int? IntNullable1 = 1;
		[SqlConstant] public static readonly int? IntNullableNull = null;

		[SqlConstant] public static readonly EnumConstants EnumValue1 = EnumConstants.Value1;
		[SqlConstant] public static readonly EnumConstants? EnumNullableValueTwo = EnumConstants.ValueTwo;
		[SqlConstant] public static readonly EnumConstants? EnumNullableNull = null;

		[SqlConstant] public static readonly DateTime DateTime010101 = new DateTime(1, 1, 1);
		[SqlConstant] public static readonly DateTime? DateTimeNullableNull = null;
		[SqlConstant] public static readonly DateTime? DateTimeNullable010101 = new DateTime(1, 1, 1);
	}
}
