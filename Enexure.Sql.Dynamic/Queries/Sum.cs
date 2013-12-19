using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Sum : Aggregate
	{
		public Sum(Expression expression)
			: base("sum", expression)
		{
		}
	}
}
