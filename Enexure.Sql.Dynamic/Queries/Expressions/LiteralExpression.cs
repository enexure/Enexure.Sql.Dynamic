using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class LiteralExpression : Expression
	{
		private readonly string expression;

		public LiteralExpression(string expression)
		{
			this.expression = expression;
		}

		public string Expression
		{
			get { return expression; }
		}
	}
}
