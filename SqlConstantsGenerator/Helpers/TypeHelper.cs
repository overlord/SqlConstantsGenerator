using System;
using System.Collections.Generic;
using System.Reflection;

namespace SqlConstantsGenerator.Helpers
{
	internal static class TypeHelper
	{
		public static Type ExtractNonNullableType<T>()
		{
			return ExtractNonNullableType(typeof(T));
		}

		public static Type ExtractNonNullableType(MemberInfo mi)
		{
			return ExtractNonNullableType(GetMemberType(mi));
		}

		public static Type ExtractNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		public static Type GetMemberType(MemberInfo mi)
		{
			if (mi is PropertyInfo pi)
			{
				return pi.PropertyType;
			}

			if (mi is FieldInfo fi)
			{
				return fi.FieldType;
			}

			throw new Exception($"Unexpected member info of type: '{mi.MemberType}'");
		}

		public static bool IsNullable(MemberInfo mi)
		{
			return IsNullable(GetMemberType(mi));
		}

		public static bool IsNullable(Type type)
		{
			return
				!type.IsValueType ||
				Nullable.GetUnderlyingType(type) != null;
		}

		public static object GetStaticValue(MemberInfo mi)
		{
			if (mi is PropertyInfo pi)
			{
				return pi.GetValue(null, null);
			}

			if (mi is FieldInfo fi)
			{
				return fi.GetValue(null);
			}

			throw new Exception($"Unexpected member info of type: '{mi.MemberType}'");
		}

		public static MemberInfo GetPropertyOrField(Type type, string memberName)
		{
			if (type.IsEnum)
			{
				return type.GetField(memberName, BindingFlags.Public | BindingFlags.Static);
			}

			var pi = type.GetProperty(memberName, BindingFlags.Public | BindingFlags.Static);
			if (pi != null)
			{
				return pi;
			}

			return type.GetField(memberName, BindingFlags.Public | BindingFlags.Static);
		}

		public static IList<MemberInfo> GetStaticMembers(Type type)
		{
			var result = new List<MemberInfo>();
			if (type.IsEnum)
			{
				result.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.Static));
				return result;
			}

			result.AddRange(type.GetProperties(BindingFlags.Public | BindingFlags.Static));
			result.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.Static));
			return result;
		}
	}
}
