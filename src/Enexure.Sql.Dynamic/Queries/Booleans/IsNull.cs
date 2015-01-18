using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class IsNull : IBoolean
	{
		private readonly IExpression expression;

		public IsNull(IExpression expression)
		{
			this.expression = expression;
		}

		public IExpression Expression
		{
			get { return expression; }
		}
	}
}
