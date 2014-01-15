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
		private readonly Select select;

		public OrderByItem(Field field)
		{
			this.order = null;
			this.select = field.AsSelf();
		}

		public OrderByItem(Select select)
		{
			this.order = null;
			this.select = select;
		}

		public OrderByItem(Field field, Order order)
		{
			this.order = order;
			this.select = field.AsSelf();
		}

		public OrderByItem(Select select, Order order)
		{
			this.order = order;
			this.select = select;
		}

		public bool ExplicitOrder
		{
			get { return order != null; }
		}

		public Order Order
		{
			get { return order != null ? order.Value : Order.Ascending; }
		}

		public Select Select
		{
			get { return select; }
		}
	}
}
