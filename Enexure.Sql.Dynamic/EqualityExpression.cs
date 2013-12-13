using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class EqualityExpression : BooleanExpression
	{
		private readonly Expression expressionLeft;
		private readonly Expression expressionRight;

		public EqualityExpression(Expression expressionLeft, Expression expressionRight)
		{
			this.expressionLeft = expressionLeft;
			this.expressionRight = expressionRight;
		}

		public Expression ExpressionLeft
		{
			get { return expressionLeft; }
		}

		public Expression ExpressionRight
		{
			get { return expressionRight; }
		}
	}
}
