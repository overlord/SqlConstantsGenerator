using System.IO;

namespace SqlConstantsGenerator.Helpers
{
	internal static class PathHelper
	{
		public static void SafeCreateDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		public static string GetSafeFilename(string filename)
		{
			return string.Join("", filename.Split(Path.GetInvalidFileNameChars()));
		}
	}
}
