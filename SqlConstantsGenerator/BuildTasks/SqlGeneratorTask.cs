using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Build.Framework;
using SqlConstantsGenerator.Engine;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	public class SqlGeneratorTask : ITask
	{
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		public string SourceAssemblyPath { get; set; }

		//absolute path to generated files
		public string DestinationFolder { get; set; }

		public string EncodedTypePreCreateCode { get; set; }
		public string EncodedTypePostCreateCode { get; set; }

		public bool Execute()
		{
			if (!Directory.Exists(DestinationFolder))
			{
				Directory.CreateDirectory(DestinationFolder);
			}

			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (o, e) => Assembly.ReflectionOnlyLoad(e.Name);

			LoadDependentAssemblies(SourceAssemblyPath);

			var assembly = Assembly.ReflectionOnlyLoadFrom(SourceAssemblyPath);

			var types = assembly.GetTypes()
				.Select(i =>
					new
					{
						UserType = i,
						ConstantProviderData = AttributeHelper.GetConstantContainerData(i)
					})
				.Where(i => i.ConstantProviderData != null)
				.ToList();

			foreach (var type in types)
			{
				var definition = SqlGenerator.GenerateDefinition(type.UserType, type.ConstantProviderData);

				var generatedSql = SqlGenerator.GenerateSqlText(
					definition,
					StringHelper.DecodeArgument(EncodedTypePreCreateCode),
					StringHelper.DecodeArgument(EncodedTypePostCreateCode)
				);

				var targetFile = Path.ChangeExtension(Path.Combine(DestinationFolder, StringHelper.GetSafeFilename(definition.ViewName)), "sql");

				File.WriteAllText(targetFile, generatedSql, Encoding.UTF8);
			}

			return true;
		}

		private void LoadDependentAssemblies(string sourceAssemblyPath)
		{
			//load all assemblies in project output folder to reflection-only context
			var directoryName = Path.GetDirectoryName(sourceAssemblyPath);

			var files = Directory.GetFiles(directoryName)
				.Where(f => StringHelper.IsEqualStrings(Path.GetExtension(f), ".dll"));

			foreach (var file in files)
			{
				Assembly.ReflectionOnlyLoadFrom(file);
			}
		}
	}
}
