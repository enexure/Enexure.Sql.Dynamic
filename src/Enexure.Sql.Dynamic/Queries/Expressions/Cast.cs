using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Cast : Expression
	{
		private readonly string type;
		private readonly IExpression expression;

		public Cast(IExpression expression, string type)
		{
			this.type = type;
			this.expression = expression;
		}

		public virtual string Type {
			get { return type; }
		}

		public IExpression Expression
		{
			get { return expression; }
		}
	}
}
