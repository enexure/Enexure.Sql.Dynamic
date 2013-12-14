using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class Conjunction : Expression, IEnumerable<BooleanExpression>
	{
		private readonly ImmutableList<BooleanExpression> expressions;

		public Conjunction()
		{
			expressions = ImmutableList<BooleanExpression>.Empty;
		}

		public Conjunction(BooleanExpression expression)
		{
			this.expressions = ImmutableList<BooleanExpression>.Empty.Add(expression);
		}

		private Conjunction(Conjunction conjuction, BooleanExpression expression)
		{
			this.expressions = conjuction.expressions.Add(expression);
		}

		public Conjunction Add(BooleanExpression booleanExpression)
		{
			return new Conjunction(this, booleanExpression);
		}

		public IEnumerable<BooleanExpression> Expressions
		{
			get { return expressions; }
		}

		public bool IsEmpty {
			get { return expressions.IsEmpty; }
		}

		public IEnumerator<BooleanExpression> GetEnumerator()
		{
			return expressions.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
