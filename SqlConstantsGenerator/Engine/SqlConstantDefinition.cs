using System.Collections.Generic;

namespace SqlConstantsGenerator.Engine
{
	public class SqlConstantDefinition
	{
		public string ViewName { get; set; }
		public IList<string> Columns { get; set; }
	}
}
