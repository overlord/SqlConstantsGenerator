﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Engine
{
	public static class SqlGenerator
	{
		internal static SqlConstantDefinition GenerateDefinition(Type type)
		{
			var constantProviderData = AttributeHelper.GetConstantProviderData(type);
			return GenerateDefinition(type, constantProviderData);
		}

		internal static SqlConstantDefinition GenerateDefinition(Type type, CustomAttributeData constantProviderData)
		{
			return new SqlConstantDefinition
			{
				ViewName = AttributeHelper.GetViewName(constantProviderData) ?? type.Name,
				Columns = TypeHelper.GetStaticMembers(type)
					.Select(GenerateConstantLine)
					.Where(s => !string.IsNullOrEmpty(s))
					.ToList()
			};
		}

		internal static string GenerateConstantLine(MemberInfo property)
		{
			var gen = new ConstantGenerator(property);

			var res = $"{gen.GetValue()} as {gen.GetName()}";

			var comment = gen.GetComment();
			if (!string.IsNullOrWhiteSpace(comment))
			{
				res += $" /* {comment} */";
			}

			return res;
		}

		internal static string GenerateSqlText(SqlConstantDefinition definition, string typePreCreateCode, string typePostCreateCode)
		{
			var eol = Environment.NewLine;
			var assemblyName = typeof(SqlGenerator).Assembly.GetName();

			var sb = new StringBuilder();

			sb.AppendLine($"--autogenerated by {assemblyName.Name} v{assemblyName.Version}");
			sb.AppendLine();

			if (!string.IsNullOrEmpty(typePreCreateCode))
			{
				sb.AppendLine(typePreCreateCode);
			}

			sb.AppendLine($"create view [{definition.ViewName}]");
			sb.AppendLine("with schemabinding");
			sb.AppendLine("as");
			sb.AppendLine("select");
			sb.Append(string.Join($",{eol}", definition.Columns.Select(c => "\t" + c)));
			sb.AppendLine();
			sb.AppendLine(";");
			sb.AppendLine("go");

			if (!string.IsNullOrEmpty(typePostCreateCode))
			{
				sb.AppendLine();
				sb.AppendLine(typePostCreateCode);
			}

			return sb.ToString();
		}
	}
}