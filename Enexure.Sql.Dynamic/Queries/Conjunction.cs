using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Conjunction : ExpressionList<Boolean>
	{
		public Conjunction()
			: base()
		{
		}

		public Conjunction(Boolean expression)
			: base(expression)
		{
		}

		private Conjunction(Conjunction list, Boolean expression)
			: base(list, expression)
		{
		}

		public Conjunction Add(Boolean booleanExpression)
		{
			return new Conjunction(this, booleanExpression);
		}
	}
}
