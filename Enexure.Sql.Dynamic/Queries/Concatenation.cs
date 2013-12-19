using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Concatenation : Expression
	{
		private readonly IEnumerable<Expression> expressions;

		public Concatenation(IEnumerable<Expression> expressions)
		{
			this.expressions = expressions;
		}

		public IEnumerable<Expression> Expressions
		{
			get { return expressions; }
		}
	}
}
