using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class OrderByClause : ExpressionList<OrderByItem>
	{
		public OrderByClause()
			: base()
		{
		}

		public OrderByClause(OrderByClause orderByClause, OrderByItem item)
			: base(orderByClause, item)
		{ 
		}

		public virtual OrderByClause Add(OrderByItem item)
		{
			return new OrderByClause(this, item);
		}

	}
}
