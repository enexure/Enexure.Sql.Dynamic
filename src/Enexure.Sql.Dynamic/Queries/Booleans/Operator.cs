using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class Operator : IBoolean
	{
		private readonly IExpression leftExpression;
		private readonly IExpression rightExpression;

		protected Operator(IExpression leftExpression, IExpression rightExpression)
		{
			this.leftExpression = leftExpression;
			this.rightExpression = rightExpression;
		}

		public abstract string OperatorSymbol { get; }

		public IExpression LeftExpression
		{
			get { return leftExpression; }
		}

		public IExpression RightExpression
		{
			get { return rightExpression; }
		}
	}
}
