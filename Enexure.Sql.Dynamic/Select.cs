using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class Select : Expression
	{
		private readonly Expression expression;
		private readonly string alias;

		protected Select()
		{
		}

		public Select(Expression expression)
		{
			this.expression = expression;
		}

		public Select(Expression expression, string alias)
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

		public Select Count()
		{
			return new Count(this);
		}

		public Select Sum()
		{
			return new Sum(this);
		}
	}
}
