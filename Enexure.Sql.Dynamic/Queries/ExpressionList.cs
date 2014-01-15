using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class ExpressionList<T> : IEnumerable<T>
	{
		protected readonly ImmutableList<T> expressions;

		protected ExpressionList()
		{
			expressions = ImmutableList<T>.Empty;
		}

		protected ExpressionList(ImmutableList<T> expressions)
		{
			this.expressions = expressions;
		}

		protected ExpressionList(T expression)
		{
			this.expressions = ImmutableList<T>.Empty.Add(expression);
		}

		protected ExpressionList(ExpressionList<T> list, T expression)
		{
			this.expressions = list.expressions.Add(expression);
		}

		protected ExpressionList(IEnumerable<T> list)
		{
			this.expressions = ImmutableList<T>.Empty.AddRange(list);
		}

		public IEnumerable<T> Expressions
		{
			get { return expressions; }
		}

		public bool IsEmpty {
			get { return expressions.IsEmpty; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return expressions.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
