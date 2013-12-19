using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class Expression
	{
		public static Expression Const(object value)
		{
			return new Constant(value);
		}

		public static Equality Eq(Expression expressionLeft, Expression expressionRight)
		{
			return new Equality(expressionLeft, expressionRight);
		}

		public static InValues In(Expression expression, IEnumerable<object> values)
		{
			return new InValues(expression, values);
		}

		public static Concatenation Concat(IEnumerable<Expression> expressions)
		{
			return new Concatenation(expressions);
		}

		public static Concatenation Concat(params Expression[] expressions)
		{
			return new Concatenation(expressions);
		}

		public Select As(string alias)
		{
			return new Select(this, alias);
		}

		public Select AsSelf()
		{
			return new Select(this, string.Empty);
		}

		public Function WithFunc(string function)
		{
			return new Function(function, this);
		}
	}
}
