using System.Collections.Generic;

namespace SqlConstantsGenerator.Engine
{
	internal class SqlConstantDefinition
	{
		public string ViewName { get; set; }
		public IList<string> Columns { get; set; }
	}
}
