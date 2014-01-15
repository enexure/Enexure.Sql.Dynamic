using System;
using System.Collections.Generic;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Conjunction : ExpressionList<IBoolean>, IBoolean
	{
		private static readonly Conjunction conjunction = new Conjunction();

		public static Conjunction Empty
		{
			get { return conjunction; }
		}

		public Conjunction()
			: base()
		{
		}

		public Conjunction(IBoolean expression)
			: base(expression)
		{
		}

		public Conjunction(IEnumerable<IBoolean> expressions)
			: base(expressions)
		{
		}

		private Conjunction(Conjunction conjunction, IBoolean expression)
			: base(conjunction, expression)
		{
		}

		public Conjunction Add(IBoolean booleanExpression)
		{
			return new Conjunction(this, booleanExpression);
		}
	}
}
