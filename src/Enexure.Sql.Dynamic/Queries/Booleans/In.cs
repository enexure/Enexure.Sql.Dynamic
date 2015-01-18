using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class In : IBoolean
	{
		private readonly IExpression expression;

		protected In(IExpression expression)
		{
			this.expression = expression;
		}

		public IExpression Expression
		{
			get { return expression; }
		}

	}
}
