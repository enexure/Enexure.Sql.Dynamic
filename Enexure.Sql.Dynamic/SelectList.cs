using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class SelectList : ExpressionList<Select>
	{
		public SelectList()
			: base()
		{
		}

		public SelectList(SelectList selectList, Select selectExpression)
			: base(selectList, selectExpression)
		{
		}

		public SelectList(SelectList selectList, Expression expression)
			: this(selectList, new Select(expression))
		{
		}

		public SelectList(SelectList selectList, IEnumerable<Expression> expressions)
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

		public virtual SelectList Add(IEnumerable<Expression> expressions)
		{
			return new SelectList(this, expressions);
		}

		public virtual SelectList Add(Expression expression)
		{
			return new SelectList(this, expression);
		}

	}
}
