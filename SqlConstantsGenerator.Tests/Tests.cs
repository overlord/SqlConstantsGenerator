﻿using System;
using System.Reflection;
using NUnit.Framework;
using SqlConstantsGenerator.Engine;
using SqlConstantsGenerator.Tests.TestData;

namespace SqlConstantsGenerator.Tests
{
	public class Tests
	{
		[Test, TestCaseSource(typeof(GenerateConstantLineTestData), nameof(GenerateConstantLineTestData.TestCases))]
		public bool GivenItem_ThenItemsGenerated(MemberInfo pi, string expected)
		{
			var generated = SqlGenerator.GenerateConstantLine(pi);
			Assert.AreEqual(expected, generated);
			return true;
		}

		[Test]
		public void GivenEnum_ThenGenerateDefinition()
		{
			var generated = SqlGenerator.GenerateDefinition(typeof(EnumConstants));
			Assert.AreEqual("EnumConstants", generated.ViewName);
			Assert.IsNotEmpty(generated.Columns);
			Assert.AreEqual(4, generated.Columns.Count);
		}

		[Test]
		public void GivenEnumNamed_ThenGenerateDefinition()
		{
			var generated = SqlGenerator.GenerateDefinition(typeof(EnumConstantsNamed));
			Assert.AreEqual("enum_constants_named", generated.ViewName);
			Assert.IsNotEmpty(generated.Columns);
			Assert.AreEqual(4, generated.Columns.Count);
		}

		[Test]
		public void GivenEnum_ThenGenerateSqlView()
		{
			const string expectedSql = @"create view [EnumConstants]
with schemabinding
as
select
	0 as Value0,
	1 as Value1,
	2 as ValueTwo,
	3 as value_3
;
go
";

			var definition = SqlGenerator.GenerateDefinition(typeof(EnumConstants));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, null, null);

			AssertEqualSql(expectedSql, generatedSql);
		}

		[Test]
		public void GivenStaticClass_ThenGenerateSqlView()
		{
			const string expectedSql = @"create view [StaticClassConstants]
with schemabinding
as
select
	null as StringNull,
	'' as StringEmpty,
	'12345' as String12345,
	'string_ololo_value' as string_ololo,
	'string_ololo_comment_value' as string_ololo_comment /* Hello! World! */,
	0 as Int0,
	1 as IntNullable1,
	null as IntNullableNull,
	1 as EnumValue1,
	2 as EnumNullableValueTwo,
	null as EnumNullableNull
;
go
";

			var definition = SqlGenerator.GenerateDefinition(typeof(StaticClassConstants));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, null, null);

			AssertEqualSql(expectedSql, generatedSql);
		}

		private static void AssertEqualSql(string expectedSql, string actualSql)
		{
			Console.WriteLine(actualSql);

			Assert.AreEqual(
				expectedSql.Replace("\r\n", "\n"),
				actualSql.Replace("\r\n", "\n")
			);
		}
	}
}
