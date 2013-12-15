using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class Count : Aggregate
	{
		private readonly Expression expression;

		public Count(Expression expression)
		{
			this.expression = expression;
		}

		public Expression Expression {
			get {
				return expression;
			}
		}

		public override string Function
		{
			get { return "count"; }
		}
	}
}
