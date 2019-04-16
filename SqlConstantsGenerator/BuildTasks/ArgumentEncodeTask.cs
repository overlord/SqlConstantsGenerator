using Microsoft.Build.Framework;
using SqlConstantsGenerator.Helpers;

namespace SqlConstantsGenerator.BuildTasks
{
	/// <summary> Build task to perform Base64 encodeing over input value </summary>
	/// <inheritdoc />
	public class ArgumentEncodeTask : TaskBase
	{
		/// <summary> Output base64-encoded value </summary>
		[Output]
		public string OutputValue { get; set; }

		/// <summary> Input value </summary>
		public string InputValue { get; set; }

		/// <inheritdoc />
		public override bool Execute()
		{
			OutputValue = StringHelper.ToBase64String(InputValue);
			return true;
		}
	}
}
