using Microsoft.Build.Framework;

namespace SqlConstantsGenerator.BuildTasks
{
	public class BuildDestinationFolderTask : ITask
	{
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		private const string DefaultDestinationFolder = "GeneratedSqlConstants";

		[Output]
		public string Path { get; set; }

		public string Value { get; set; }
		public string ProjectDir { get; set; }

		public bool Execute()
		{
			if (string.IsNullOrEmpty(Value))
			{
				Path = System.IO.Path.Combine(ProjectDir, DefaultDestinationFolder);
			}
			else
			{
				Path = System.IO.Path.GetFullPath(Value.TrimEnd('\\'));
			}

			return true;
		}
	}
}
