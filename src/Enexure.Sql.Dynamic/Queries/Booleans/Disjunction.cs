using System;
using System.Collections.Generic;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Disjunction : ExpressionList<IBoolean>, IBoolean
	{
		private static readonly Disjunction disjunction = new Disjunction();

		public static Disjunction Empty
		{
			get { return disjunction; }
		}

		public Disjunction()
			: base()
		{
		}

		public Disjunction(IBoolean expression)
			: base(expression)
		{
		}

		public Disjunction(IEnumerable<IBoolean> expressions)
			: base(expressions)
		{
		}

		private Disjunction(Disjunction disjunction, IBoolean expression)
			: base(disjunction, expression)
		{
		}



		public Disjunction Add(IBoolean booleanExpression)
		{
			return new Disjunction(this, booleanExpression);
		}
	}
}
