using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Count : Aggregate
	{
		public Count(IExpression expression)
			: base("count", expression)
		{
		}

		public Count(Star star)
			: base("count", new LiteralExpression("*"))
		{
		}
	}
}
