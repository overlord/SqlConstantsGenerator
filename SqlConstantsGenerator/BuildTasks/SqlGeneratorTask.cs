using SqlConstantsGenerator.Engine;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	/// <summary> Generate Sql Constant Build Task  </summary>
	public class SqlGeneratorTask : TaskBase
	{
		/// <summary> Absolute path to source assembly </summary>
		public string SourceAssemblyPath { get; set; }

		/// <summary> Absolute path for generated files </summary>
		public string DestinationFolder { get; set; }

		/// <summary> Base64-encoded prefix sql code </summary>
		public string EncodedPrefixSql { get; set; }

		/// <summary> Base64-encoded postfix sql code </summary>
		public string EncodedPostfixSql { get; set; }

		/// <inheritdoc />
		public override bool Execute()
		{
			return new SqlGeneratorTaskWorker(
				DestinationFolder,
				SourceAssemblyPath,
				StringHelper.FromBase64String(EncodedPrefixSql),
				StringHelper.FromBase64String(EncodedPostfixSql),
				LogMessage
			).Execute();
		}
	}
}
