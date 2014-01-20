using System;
using System.Collections.Generic;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Conjunction : ExpressionList<IBoolean>, IBoolean
	{
		private readonly bool rootConjunction;
		private static readonly Conjunction conjunction = new Conjunction();

		public static Conjunction Empty
		{
			get { return conjunction; }
		}

		public bool RootConjunction
		{
			get { return rootConjunction; }
		}

		public Conjunction()
			: base()
		{
		}

		public Conjunction(bool rootConjunction)
			: base()
		{
			this.rootConjunction = rootConjunction;
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
			this.rootConjunction = conjunction.RootConjunction;
		}

		public Conjunction Add(IBoolean booleanExpression)
		{
			return new Conjunction(this, booleanExpression);
		}
	}
}
