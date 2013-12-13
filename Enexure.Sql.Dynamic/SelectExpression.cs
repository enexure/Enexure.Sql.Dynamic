using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class SelectExpression : Expression
	{
		private readonly Expression expression;
		private readonly string alias;

		public SelectExpression(Expression expression)
		{
			this.expression = expression;
		}

		public SelectExpression(Expression expression, string alias)
		{
			this.expression = expression;
			this.alias = alias;
		}

		public Expression Expression
		{
			get { return expression; }
		}

		public string Alias
		{
			get { return alias; }
		}
	}
}
