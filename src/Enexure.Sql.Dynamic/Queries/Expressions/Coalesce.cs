using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Coalesce : ExpressionList<IExpression>, IExpression
	{
		private readonly IEnumerable<IExpression> expressions;

		public Coalesce(params IExpression[] expressions)
			: base(expressions)
		{
		}

		public Coalesce(IEnumerable<IExpression> expressions)
			: base(expressions)
		{
		}

		private Coalesce(Coalesce coalesce, IExpression expression)
			: base(coalesce, expression)
		{
		}

		public Select As(string alias)
		{
			return new Select(this, alias);
		}

		public Select AsSelf()
		{
			return new Select(this, string.Empty);
		}

		public Function WithFunc(string function)
		{
			return new Function(function, this);
		}

		public IExpression CastTo(string type)
		{
			return new Cast(this, type);
		}
	}
}
