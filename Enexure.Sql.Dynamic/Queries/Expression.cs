using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Expression : IExpression
	{
		public static IExpression Const(object value)
		{
			return new Constant(value);
		}

		public static Equality Eq(IExpression expressionLeft, object value)
		{
			return new Equality(expressionLeft, new Constant(value));
		}

		public static Equality Eq(IExpression expressionLeft, IExpression expressionRight)
		{
			return new Equality(expressionLeft, expressionRight);
		}

		public static InValues In(IExpression expression, IEnumerable<object> values)
		{
			return new InValues(expression, values);
		}

		public static Concatenation Concat(IEnumerable<IExpression> expressions)
		{
			return new Concatenation(expressions);
		}

		public static Concatenation Concat(params IExpression[] expressions)
		{
			return new Concatenation(expressions);
		}

		public static Conjunction Conjunction()
		{
			return Queries.Conjunction.Empty;
		}

		public static Disjunction Disjunction()
		{
			return Queries.Disjunction.Empty;
		}
	
		// Non static

		public Count Count()
		{
			return new Count(this);
		}

		public Sum Sum()
		{
			return new Sum(this);
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
