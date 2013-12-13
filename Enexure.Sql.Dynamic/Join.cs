using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class Join
	{
		private readonly TableSource source;
		private readonly BooleanExpression expression;

		public Join(TableSource source, BooleanExpression expression)
		{
			this.source = source;
			this.expression = expression;
		}

		public TableSource Source
		{
			get { return source; }
		}

		public BooleanExpression Expression
		{
			get { return expression; }
		}
	}
}
