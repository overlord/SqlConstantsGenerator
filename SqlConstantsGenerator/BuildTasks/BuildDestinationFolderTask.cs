using System.Reflection;
using Microsoft.Build.Framework;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	/// <summary> Task for building destination path for output scripts </summary>
	/// <inheritdoc />
	public class BuildDestinationFolderTask : ITask
	{
		/// <inheritdoc />
		public IBuildEngine BuildEngine { get; set; }

		/// <inheritdoc />
		public ITaskHost HostObject { get; set; }

		private const string DefaultDestinationFolder = "GeneratedSqlConstants";

		/// <summary> Input destination folder </summary>
		public string InputValue { get; set; }

		/// <summary> Output destination folder </summary>
		[Output]
		public string OutputValue { get; set; }

		/// <summary> Path to sln-file </summary>
		public string SolutionDir { get; set; }

		/// <summary> Path to proj-file </summary>
		public string ProjectDir { get; set; }

		/// <summary> Absolute path to source assembly </summary>
		public string SourceAssemblyPath { get; set; }

		/// <inheritdoc />
		public bool Execute()
		{
			if (!string.IsNullOrWhiteSpace(InputValue))
			{
				OutputValue = System.IO.Path.GetFullPath(CustomReplace(InputValue).TrimEnd('\\'));
				return true;
			}

			if (!string.IsNullOrWhiteSpace(SourceAssemblyPath))
			{
				var assembly = Assembly.ReflectionOnlyLoadFrom(SourceAssemblyPath);
				var generatorOptions = AttributeHelper.GetGeneratorOptions(assembly);

				if (!string.IsNullOrWhiteSpace(generatorOptions?.DestinationFolder))
				{
					OutputValue = System.IO.Path.GetFullPath(CustomReplace(generatorOptions.DestinationFolder).TrimEnd('\\'));
					return true;
				}
			}

			OutputValue = System.IO.Path.Combine(ProjectDir, DefaultDestinationFolder);
			return true;
		}

		private string CustomReplace(string s)
		{
			s = s?.Replace("$SolutionDir$", SolutionDir);
			s = s?.Replace("$ProjectDir$", ProjectDir);
			return s;
		}
	}
}
