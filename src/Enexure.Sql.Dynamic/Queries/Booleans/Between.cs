using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Between : IBoolean
	{
		private readonly IExpression testExpression;
		private readonly IExpression leftExpression;
		private readonly IExpression rightExpression;

		public Between(IExpression testExpression, IExpression leftExpression, IExpression rightExpression)
		{
			this.testExpression = testExpression;
			this.leftExpression = leftExpression;
			this.rightExpression = rightExpression;
		}

		public IExpression LeftExpression
		{
			get { return leftExpression; }
		}

		public IExpression RightExpression
		{
			get { return rightExpression; }
		}

		public IExpression TestExpression
		{
			get { return testExpression; }
		}
	}
}
