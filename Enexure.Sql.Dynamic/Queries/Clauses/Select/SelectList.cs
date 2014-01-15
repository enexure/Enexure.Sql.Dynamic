using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class SelectList : ExpressionList<Select>
	{
		private static readonly SelectList empty = new SelectList();
		public static SelectList Empty {
			get { return empty; }
		}

		public SelectList()
			: base()
		{
		}

		public SelectList(SelectList selectList, Select selectExpression)
			: base(selectList, selectExpression)
		{
		}

		public SelectList(SelectList selectList, IExpression expression)
			: this(selectList, new Select(expression))
		{
		}

		public SelectList(SelectList selectList, IEnumerable<IExpression> expressions)
			: base(selectList.expressions.AddRange(
				expressions.Select(x => {
					var expression = x as Select;
					return expression ?? new Select(x);
				})
			))
		{
		}

		public SelectList Add(Select selectExpression)
		{
			return new SelectList(this, selectExpression);
		}

		public virtual SelectList Add(IEnumerable<IExpression> expressions)
		{
			return new SelectList(this, expressions);
		}

		public virtual SelectList Add(IExpression expression)
		{
			return new SelectList(this, expression);
		}

	}
}
