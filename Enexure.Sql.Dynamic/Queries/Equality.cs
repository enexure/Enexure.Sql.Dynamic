using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Equality : Expression, IBoolean
	{
		private readonly IExpression expressionLeft;
		private readonly IExpression expressionRight;

		public Equality(IExpression expressionLeft, IExpression expressionRight)
		{
			this.expressionLeft = expressionLeft;
			this.expressionRight = expressionRight;
		}

		public IExpression ExpressionLeft
		{
			get { return expressionLeft; }
		}

		public IExpression ExpressionRight
		{
			get { return expressionRight; }
		}
	}
}
