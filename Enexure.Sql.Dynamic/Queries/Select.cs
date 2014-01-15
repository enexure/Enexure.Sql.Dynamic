using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Select
	{
		private readonly IExpression expression;
		private readonly string alias;

		protected Select()
		{
		}

		public Select(IExpression expression)
		{
			this.expression = expression;
		}

		public Select(IExpression expression, string alias)
		{
			this.expression = expression;
			this.alias = alias;
		}

		public IExpression Expression
		{
			get { return expression; }
		}

		public string Alias
		{
			get { return alias; }
		}

	}
}
