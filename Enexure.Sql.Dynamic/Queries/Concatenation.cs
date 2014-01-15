using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Concatenation : Expression
	{
		private readonly IEnumerable<IExpression> expressions;

		public Concatenation(IEnumerable<IExpression> expressions)
		{
			this.expressions = expressions;
		}

		public IEnumerable<IExpression> Expressions
		{
			get { return expressions; }
		}
	}
}
