using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Tests.TestData
{
	public class GenerateConstantLineTestData
	{
		public static readonly IEnumerable TestCases =
			new (MemberInfo Property, string Expected)[]
				{
					(TypeHelper.GetPropertyOrField(typeof(EnumConstants), nameof(EnumConstants.Value0)), "0 as Value0"),
					(TypeHelper.GetPropertyOrField(typeof(EnumConstants), nameof(EnumConstants.Value1)), "1 as Value1"),
					(TypeHelper.GetPropertyOrField(typeof(EnumConstants), nameof(EnumConstants.ValueTwo)), "2 as ValueTwo"),
					(TypeHelper.GetPropertyOrField(typeof(EnumConstants), nameof(EnumConstants.ValueThree)), "3 as value_3"),

					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.StringNull)), "null as StringNull"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.StringEmpty)), "'' as StringEmpty"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.String12345)), "'12345' as String12345"),

					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.StringOlolo)), "'string_ololo_value' as string_ololo"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.StringOloloComment)), "'string_ololo_comment_value' as string_ololo_comment /* Hello! World! */"),

					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.Int0)), "0 as Int0"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.IntNullable1)), "1 as IntNullable1"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.IntNullableNull)), "null as IntNullableNull"),

					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.EnumValue1)), "1 as EnumValue1"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.EnumNullableValueTwo)), "2 as EnumNullableValueTwo"),
					(TypeHelper.GetPropertyOrField(typeof(StaticClassConstants), nameof(StaticClassConstants.EnumNullableNull)), "null as EnumNullableNull"),
				}
				.Select(item => new TestCaseData(item.Property, item.Expected).Returns(true));
	}
}
