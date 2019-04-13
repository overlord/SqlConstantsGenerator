using System;
using System.Text;

namespace SqlConstantsGenerator.Helpers
{
	internal static class StringHelper
	{
		public static bool IsEqualStrings(string s1, string s2)
		{
			return string.Compare(s1, s2, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		public static string ToBase64String(string s)
		{
			return string.IsNullOrEmpty(s) ? null : Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
		}

		public static string FromBase64String(string s)
		{
			return !string.IsNullOrEmpty(s) ? Encoding.UTF8.GetString(Convert.FromBase64String(s)) : null;
		}
	}
}
