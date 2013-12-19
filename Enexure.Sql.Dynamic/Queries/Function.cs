using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Function : Expression
	{
		private readonly string function;
		private readonly Expression expression;

		public Function(string function, Expression expression)
		{
			this.function = function;
			this.expression = expression;
		}

		public virtual string FunctionName {
			get { return function; }
		}

		public Expression Expression
		{
			get { return expression; }
		}
	}
}
