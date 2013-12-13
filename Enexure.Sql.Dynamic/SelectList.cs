using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class SelectList : Expression, IEnumerable<SelectExpression>
	{
		private readonly ImmutableList<SelectExpression> selectList;

		public SelectList()
		{
			selectList = ImmutableList<SelectExpression>.Empty;
		}

		private SelectList(ImmutableList<SelectExpression> selectList)
		{
			this.selectList = selectList;
		}

		public SelectList(SelectList selectList, SelectExpression selectExpression)
		{
			this.selectList = selectList.selectList.Add(selectExpression);
		}

		public SelectList(SelectList selectList, Expression expression)
			: this(selectList, new SelectExpression(expression))
		{
		}

		public SelectList(SelectList selectList, IEnumerable<Expression> expressions)
		{
			this.selectList = selectList.selectList.AddRange(expressions.Select(x => {
				var expression = x as SelectExpression;
				return expression ?? new SelectExpression(x);
			}));
		}

		public SelectList Add(SelectExpression selectExpression)
		{
			return new SelectList(this, selectExpression);
		}

		public SelectList Add(IEnumerable<Expression> expressions)
		{
			return new SelectList(this, expressions);
		}

		public SelectList Add(Expression expression)
		{
			return new SelectList(this, expression);
		}

		public IEnumerator<SelectExpression> GetEnumerator()
		{
			return selectList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
