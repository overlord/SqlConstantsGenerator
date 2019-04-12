using SqlConstantsGenerator.Attributes;

namespace SqlConstantsGenerator.Tests.TestData
{
	/// <summary> Роли пользователей </summary>
	[SqlConstantContainer(ViewName = "const_user_role_type")]
	internal enum UserRoleType
	{
		/// <summary> Неизвестная роль </summary>
		[SqlConstant(ColumnName = "unknown")] Unknown = 0,

		/// <summary> Клиент </summary>
		[SqlConstant(ColumnName = "client")] Client = 1,

		/// <summary> Супер администратор </summary>
		[SqlConstant(ColumnName = "super_admin")]
		SuperAdmin = 2,

		/// <summary> Технический администратор </summary>
		[SqlConstant(ColumnName = "technical_admin")]
		TechnicalAdmin = 3,

		/// <summary> Маркетолог </summary>
		[SqlConstant(ColumnName = "marketer")] Marketer = 4,

		/// <summary> Тестировщик </summary>
		[SqlConstant(ColumnName = "tester")] Tester = 5,

		/// <summary> Клиентская поддержка </summary>
		[SqlConstant(ColumnName = "client_support")]
		ClientSupport = 6,
	}
}
