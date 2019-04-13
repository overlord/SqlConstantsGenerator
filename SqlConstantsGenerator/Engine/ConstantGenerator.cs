using System;
using System.Reflection;
using SqlConstantsGenerator.Attributes;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Engine
{
	internal class ConstantGenerator
	{
		private readonly MemberInfo _propertyInfo;
		private readonly Type _propertyInfoType;
		private readonly SqlConstantAttribute _constant;

		public ConstantGenerator(MemberInfo propertyInfo)
		{
			_propertyInfo = propertyInfo;
			_propertyInfoType = TypeHelper.ExtractNonNullableType(_propertyInfo);
			_constant = AttributeHelper.GetConstant(_propertyInfo);
		}

		public string GetName()
		{
			return _constant?.ColumnName ?? _propertyInfo.Name;
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

			if (_propertyInfoType == typeof(DateTime))
			{
				return $"convert(datetime, '{((DateTime)propValue).ToString("yyyy-MM-dd HH:mm:ss")}', 120)";
			}

			return "???";
		}

		public string GetComment()
		{
			return _constant?.Comment;
		}
	}
}
