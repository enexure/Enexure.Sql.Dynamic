using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
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

		public Count Count()
		{
			return new Count(this);
		}

		public Sum Sum()
		{
			return new Sum(this);
		}
	}
}
