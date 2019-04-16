using Microsoft.Build.Framework;

namespace SqlConstantsGenerator.BuildTasks
{
	/// <summary> Base Build Task </summary>
	/// <inheritdoc />
	public abstract class TaskBase : ITask
	{
		/// <inheritdoc />
		public IBuildEngine BuildEngine { get; set; }

		/// <inheritdoc />
		public ITaskHost HostObject { get; set; }

		/// <inheritdoc />
		public abstract bool Execute();

		/// <summary> Log message </summary>
		protected void LogMessage(string msg)
		{
			BuildEngine.LogMessageEvent(new BuildMessageEventArgs(msg, null, GetType().Name, MessageImportance.Normal));
		}
	}
}
