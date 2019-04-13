using Microsoft.Build.Framework;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	public class ArgumentEncodeTask : ITask
	{
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		[Output]
		public string EncodedArgument { get; set; }

		public string Value { get; set; }

		public bool Execute()
		{
			EncodedArgument = StringHelper.ToBase64String(Value);
			return true;
		}
	}
}
