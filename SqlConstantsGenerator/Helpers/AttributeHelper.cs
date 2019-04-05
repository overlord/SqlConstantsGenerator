﻿using System;
using System.Linq;
using System.Reflection;
using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Helpers
{
	internal static class AttributeHelper
	{
		public static CustomAttributeData GetConstantProviderData(Type type)
		{
			return GetAttributeData<SqlConstantProviderAttribute>(type);
		}

		public static string GetViewName(CustomAttributeData data)
		{
			return GetAttributeArgumentValue(data, nameof(SqlConstantProviderAttribute.ViewName))?.ToString();
		}

		public static string GetColumnName(MemberInfo pi)
		{
			var customAttributeData = GetAttributeData<SqlConstantAttribute>(pi);
			return GetAttributeValue<string>(customAttributeData, nameof(SqlConstantAttribute.ColumnName));
		}

		public static string GetColumnComment(MemberInfo pi)
		{
			var customAttributeData = GetAttributeData<SqlConstantAttribute>(pi);
			return GetAttributeValue<string>(customAttributeData, nameof(SqlConstantAttribute.Comment));
		}

		private static TResult GetAttributeValue<TResult>(CustomAttributeData data, string attrName)
		{
			var attrValue = GetAttributeArgumentValue(data, attrName);
			if (attrValue == null)
				return default(TResult);

			var type = TypeHelper.ExtractNonNullableType<TResult>();
			if (type.IsEnum)
			{
				return (TResult)Enum.ToObject(type, attrValue);
			}

			return (TResult)Convert.ChangeType(attrValue, type);
		}

		public static CustomAttributeData GetAttributeData<TAttribute>(MemberInfo pi)
			where TAttribute: Attribute
		{
			foreach (var data in pi.GetCustomAttributesData())
			{
				if (StringHelper.IsEqualStrings(data.AttributeType.FullName, typeof(TAttribute).FullName))
				{
					return data;
				}
			}

			return null;
		}

		private static object GetAttributeArgumentValue(CustomAttributeData attr, string argName)
		{
			return attr?.NamedArguments?
				.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName, argName))
				.TypedValue.Value;
		}
	}
}