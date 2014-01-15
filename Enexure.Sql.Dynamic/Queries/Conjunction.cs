using System;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Conjunction : ExpressionList<IBoolean>
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
