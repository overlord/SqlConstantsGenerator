﻿using Microsoft.Build.Framework;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	public class ArgumentEncodeTask : ITask
	{
		[Output]
		public string EncodedArgument { get; set; }

		public string Value { get; set; }

		public bool Execute()
		{
			EncodedArgument = !string.IsNullOrEmpty(Value) ? StringHelper.EncodeArgument(Value) : null;
			return true;
		}

		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }
	}
}
