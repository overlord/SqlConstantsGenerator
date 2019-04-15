﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Engine
{
	internal static class SqlGenerator
	{
		private const string ViewNamePlaceholder = "$viewname$";

		internal static SqlConstantDefinition GenerateDefinition(Type type)
		{
			var constantContainer = AttributeHelper.GetConstantContainer(type);
			if (constantContainer == null)
			{
				return null;
			}

			return new SqlConstantDefinition
			{
				ViewName = constantContainer.ViewName ?? type.Name,
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

		internal static string GenerateSqlText(SqlConstantDefinition definition, string prefixSql, string postfixSql)
		{
			prefixSql = prefixSql?.Replace(ViewNamePlaceholder, definition.ViewName);
			postfixSql = postfixSql?.Replace(ViewNamePlaceholder, definition.ViewName);

			var eol = Environment.NewLine;
			var assemblyName = typeof(SqlGenerator).Assembly.GetName();

			var sb = new StringBuilder();

			sb.AppendLine($"--autogenerated by {assemblyName.Name} v{assemblyName.Version}");
			sb.AppendLine();

			if (!string.IsNullOrEmpty(prefixSql))
			{
				sb.AppendLine(prefixSql);
				sb.AppendLine();
			}

			sb.AppendLine($"create view [{definition.ViewName}]");
			sb.AppendLine("with schemabinding");
			sb.AppendLine("as");
			sb.AppendLine("select");
			sb.Append(string.Join($",{eol}", definition.Columns.Select(c => "\t" + c)));
			sb.AppendLine();
			sb.AppendLine(";");
			sb.AppendLine("go");
			sb.AppendLine();

			if (!string.IsNullOrEmpty(postfixSql))
			{
				sb.AppendLine(postfixSql);
				sb.AppendLine();
			}

			return sb.ToString().Trim();
		}
	}
}
