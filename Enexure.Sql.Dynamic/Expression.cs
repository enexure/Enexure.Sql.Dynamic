using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
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
	}
}
