using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class Count : Aggregate
	{
		public Count(Expression expression)
			: base(expression)
		{
		}

		public override string Function
		{
			get { return "count"; }
		}
	}
}
