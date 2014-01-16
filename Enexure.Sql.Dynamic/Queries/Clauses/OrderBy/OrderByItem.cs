using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class OrderByItem
	{
		private readonly Order? order;
		private readonly IExpression expression;

		public OrderByItem(Field field)
		{
			this.order = null;
			this.expression = field;
		}

		public OrderByItem(IExpression expression)
		{
			this.order = null;
			this.expression = expression;
		}

		public OrderByItem(Field field, Order order)
		{
			this.order = order;
			this.expression = field;
		}

		public OrderByItem(IExpression expression, Order order)
		{
			this.order = order;
			this.expression = expression;
		}

		public bool ExplicitOrder
		{
			get { return order != null; }
		}

		public Order Order
		{
			get { return order != null ? order.Value : Order.Ascending; }
		}

		public IExpression Expression
		{
			get { return expression; }
		}
	}
}
