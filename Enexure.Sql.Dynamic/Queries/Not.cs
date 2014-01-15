using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Not : IBoolean
	{
		private readonly IBoolean expression;

		public Not(IBoolean expression)
		{
			this.expression = expression;
		}

		public IBoolean Expression
		{
			get { return expression; }
		}
	}
}
