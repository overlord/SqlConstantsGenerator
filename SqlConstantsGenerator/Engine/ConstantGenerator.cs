using System;
using System.Reflection;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Engine
{
	internal class ConstantGenerator
	{
		private readonly MemberInfo _propertyInfo;
		private readonly Type _propertyInfoType;

		public ConstantGenerator(MemberInfo propertyInfo)
		{
			_propertyInfo = propertyInfo;
			_propertyInfoType = TypeHelper.ExtractNonNullableType(_propertyInfo);
		}

		public string GetName()
		{
			return AttributeHelper.GetColumnName(_propertyInfo) ?? _propertyInfo.Name;
		}

		public string GetValue()
		{
			var isNullable = TypeHelper.IsNullable(_propertyInfo);
			var propValue = TypeHelper.GetStaticValue(_propertyInfo);

			if (isNullable && ReferenceEquals(propValue, null))
			{
				return "null";
			}

			if (ReferenceEquals(propValue, null))
			{
				throw new Exception($"Value of '{GetName()}' cannot be null");
			}

			if (_propertyInfoType.IsEnum)
			{
				return $"{(int)propValue}";
			}
			
			if (_propertyInfoType == typeof(int))
			{
				return $"{propValue}";
			}

			if (_propertyInfoType == typeof(string))
			{
				return $"'{propValue}'";
			}

			return "???";
		}

		public string GetComment()
		{
			return AttributeHelper.GetColumnComment(_propertyInfo);
		}
	}
}
