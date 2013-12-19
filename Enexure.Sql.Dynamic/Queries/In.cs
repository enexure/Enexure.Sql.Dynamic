using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class In : Boolean
	{
		private readonly Expression expression;

		public In(Expression expression)
		{
			this.expression = expression;
		}

		public Expression Expression
		{
			get { return expression; }
		}

	}
}
