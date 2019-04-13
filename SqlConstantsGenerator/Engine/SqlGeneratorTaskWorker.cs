using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.Engine
{
	internal class SqlGeneratorTaskWorker
	{
		private readonly string _destinationFolder;
		private readonly string _sourceAssemblyPath;
		private readonly string _prefixSql;
		private readonly string _postfixSql;
		private readonly Action<string> _logger;

		public SqlGeneratorTaskWorker(
			string destinationFolder,
			string sourceAssemblyPath,
			string prefixSql,
			string postfixSql,
			Action<string> logger)
		{
			_destinationFolder = destinationFolder;
			_sourceAssemblyPath = sourceAssemblyPath;
			_prefixSql = prefixSql;
			_postfixSql = postfixSql;
			_logger = logger;
		}

		public bool Execute()
		{
			var generatedItems = Generate();

			PathHelper.SafeCreateDirectory(_destinationFolder);
			foreach (var item in generatedItems)
			{
				File.WriteAllText(item.TargetPath, item.Sql, Encoding.UTF8);
			}

			return true;
		}

		private Assembly CustomLoad(string assembly)
		{
			// ??? load all assemblies in project output folder to reflection-only context

			//return Assembly.ReflectionOnlyLoad(assembly);
			return Assembly.Load(assembly);
		}
		private Assembly CustomLoadFrom(string path)
		{
			// ??? load all assemblies in project output folder to reflection-only context

			//return Assembly.ReflectionOnlyLoadFrom(path);
			return Assembly.LoadFrom(path);
		}

		internal IList<(SqlConstantDefinition Definition, string Sql, string TargetPath)> Generate()
		{
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (o, e) => CustomLoad(e.Name);
			AppDomain.CurrentDomain.AssemblyResolve += (o, e) => CustomLoad(e.Name);
			LoadDependentAssemblies(_sourceAssemblyPath);

			var assembly = CustomLoadFrom(_sourceAssemblyPath);

			var types = assembly.GetTypes()
				.Select(i =>
					new
					{
						UserType = i,
						ConstantProviderData = AttributeHelper.GetConstantContainerData(i)
					})
				.Where(i => i.ConstantProviderData != null)
				.ToList();

			_logger?.Invoke($"Found {types.Count} types to generate");

			var result = new List<(SqlConstantDefinition Definition, string Sql, string TargetPath)>();

			foreach (var type in types)
			{
				var definition = SqlGenerator.GenerateDefinition(type.UserType, type.ConstantProviderData);
				var generatedSql = SqlGenerator.GenerateSqlText(definition, _prefixSql, _postfixSql);
				var targetFile = Path.ChangeExtension(Path.Combine(_destinationFolder, PathHelper.GetSafeFilename(definition.ViewName)), "sql");
				result.Add((definition, generatedSql, targetFile));
			}

			return result;
		}

		private void LoadDependentAssemblies(string sourceAssemblyPath)
		{
			var directoryName = Path.GetDirectoryName(sourceAssemblyPath);

			var files = Directory.GetFiles(directoryName)
				.Where(i => StringHelper.IsEqualStrings(Path.GetExtension(i), ".dll"));

			foreach (var file in files)
			{
				CustomLoadFrom(file);
			}
		}
	}
}
