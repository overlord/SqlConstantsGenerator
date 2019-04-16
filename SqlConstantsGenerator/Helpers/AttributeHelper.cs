using System;
using System.Linq;
using System.Reflection;
using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Helpers
{
	internal static class AttributeHelper
	{
		public static SqlConstantContainerAttribute GetConstantContainer(Type type)
		{
			var data = GetAttributeData<SqlConstantContainerAttribute>(type);
			if (data == null)
			{
				return null;
			}

			return new SqlConstantContainerAttribute
			{
				ViewName = GetAttributeArgumentValue<string>(data, nameof(SqlConstantContainerAttribute.ViewName)),
				Comment = GetAttributeArgumentValue<string>(data, nameof(SqlConstantContainerAttribute.Comment)),
			};
		}

		public static SqlConstantsGeneratorOptionsAttribute GetGeneratorOptions(Assembly assembly)
		{
			var data = GetAttributeData<SqlConstantsGeneratorOptionsAttribute>(assembly);
			if (data == null)
			{
				return null;
			}

			return new SqlConstantsGeneratorOptionsAttribute
			{
				DestinationFolder = GetAttributeArgumentValue<string>(data, nameof(SqlConstantsGeneratorOptionsAttribute.DestinationFolder)),
				PrefixSql = GetAttributeArgumentValue<string>(data, nameof(SqlConstantsGeneratorOptionsAttribute.PrefixSql)),
				PostfixSql = GetAttributeArgumentValue<string>(data, nameof(SqlConstantsGeneratorOptionsAttribute.PostfixSql)),
			};
		}

		public static SqlConstantAttribute GetConstant(MemberInfo pi)
		{
			var data = GetAttributeData<SqlConstantAttribute>(pi);
			if (data == null)
			{
				return null;
			}

			return new SqlConstantAttribute
			{
				ColumnName = GetAttributeArgumentValue<string>(data, nameof(SqlConstantAttribute.ColumnName)),
				Comment = GetAttributeArgumentValue<string>(data, nameof(SqlConstantAttribute.Comment)),
			};
		}

		private static TResult GetAttributeArgumentValue<TResult>(CustomAttributeData data, string attributeArgumentName)
		{
			var attrValue = data?.NamedArguments?
				.FirstOrDefault(i => StringHelper.IsEqualStrings(i.MemberName, attributeArgumentName))
				.TypedValue.Value;

			if (attrValue == null)
			{
				return default(TResult);
			}

			var type = TypeHelper.ExtractNonNullableType<TResult>();
			if (type.IsEnum)
			{
				return (TResult)Enum.ToObject(type, attrValue);
			}

			return (TResult)Convert.ChangeType(attrValue, type);
		}

		public static CustomAttributeData GetAttributeData<TAttribute>(Assembly assembly)
			where TAttribute: Attribute
		{
			//!_! 'i.AttributeType' can be ReflectionOnlyTime and 'typeof(TAttribute)' can be RuntimeType,
			//!_! so we must compare them by FullName
			return assembly.GetCustomAttributesData()
				.FirstOrDefault(i => i.AttributeType.FullName == typeof(TAttribute).FullName);
		}

		public static CustomAttributeData GetAttributeData<TAttribute>(MemberInfo pi)
			where TAttribute: Attribute
		{
			//!_! 'i.AttributeType' can be ReflectionOnlyTime and 'typeof(TAttribute)' can be RuntimeType,
			//!_! so we must compare them by FullName
			return pi.GetCustomAttributesData()
				.FirstOrDefault(i => i.AttributeType.FullName == typeof(TAttribute).FullName);
		}

		public static bool HasAttribute<TAttribute>(Type type)
			where TAttribute: Attribute
		{
			return GetAttributeData<TAttribute>(type) != null;
		}
	}
}
