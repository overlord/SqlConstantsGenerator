using System;
using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.MsBuildTests
{
	[SqlConstantContainer]
	internal static class MsBuildTestConstants
	{
		[SqlConstant] public const string FirstName = "John";
		[SqlConstant] public const string LastName = "Snow";
		[SqlConstant] public const string FavFood = "Pizza";
		[SqlConstant] public const int FavNumber = 12345;
		[SqlConstant] public static readonly DateTime BirthDate = new DateTime(2000, 01, 01, 18, 35, 40);
	}
}
