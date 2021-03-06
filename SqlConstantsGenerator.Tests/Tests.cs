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
			const string expectedSql = @"--autogenerated by SqlConstantsGenerator v1.0.0.0

create view [EnumConstants]
with schemabinding
as
select
	0 as Value0,
	1 as Value1,
	2 as ValueTwo,
	3 as value_3
;
go";

			var definition = SqlGenerator.GenerateDefinition(typeof(EnumConstants));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, null, null);

			AssertEqualSql(expectedSql, generatedSql);
		}

		[Test]
		public void GivenEnumNamedPrefixAndPostfix_ThenGenerateSqlView()
		{
			const string sqlPrefix = @"if(object_id(N'[$viewname$]', N'V') is not null) drop view [$viewname$];
go";
			const string sqlPostfix = @"print 'View [$viewname$] created successfully.';
go";
			const string expectedSql = @"--autogenerated by SqlConstantsGenerator v1.0.0.0

if(object_id(N'[enum_constants_named]', N'V') is not null) drop view [enum_constants_named];
go

create view [enum_constants_named]
with schemabinding
as
select
	0 as Value0,
	1 as Value1,
	2 as ValueTwo,
	3 as value_3
;
go

print 'View [enum_constants_named] created successfully.';
go";

			var definition = SqlGenerator.GenerateDefinition(typeof(EnumConstantsNamed));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, sqlPrefix, sqlPostfix);

			AssertEqualSql(expectedSql, generatedSql);
		}

		[Test]
		public void GivenStaticClass_ThenGenerateSqlView()
		{
			const string expectedSql = @"--autogenerated by SqlConstantsGenerator v1.0.0.0

create view [StaticClassConstants]
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
	null as EnumNullableNull,
	convert(datetime, '0001-01-01 00:00:00', 120) as DateTime010101,
	null as DateTimeNullableNull,
	convert(datetime, '0001-01-01 00:00:00', 120) as DateTimeNullable010101
;
go";

			var definition = SqlGenerator.GenerateDefinition(typeof(StaticClassConstants));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, null, null);

			AssertEqualSql(expectedSql, generatedSql);
		}

		[Test]
		public void GivenUserRoleType_ThenGenerateSqlView()
		{
			const string expectedSql = @"--autogenerated by SqlConstantsGenerator v1.0.0.0

create view [const_user_role_type]
with schemabinding
as
select
	0 as unknown,
	1 as client,
	2 as super_admin,
	3 as technical_admin,
	4 as marketer,
	5 as tester,
	6 as client_support
;
go";

			var definition = SqlGenerator.GenerateDefinition(typeof(UserRoleType));
			var generatedSql = SqlGenerator.GenerateSqlText(definition, null, null);

			AssertEqualSql(expectedSql, generatedSql);
		}

		[Test]
		public void GivenExecutingAssembly_ThenGenerateThroughBuildTask()
		{
			var worker = new SqlGeneratorTaskWorker("C:\\temp\\DbConstants", Assembly.GetExecutingAssembly().Location, null, null, null);
			var generatedItems = worker.Generate();

			//AssertEqualSql(expectedSql, generatedSql);
		}

		// ------------------------------------------------------------------------------------------

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
