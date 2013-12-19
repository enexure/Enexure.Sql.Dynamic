using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class GroupByClause : ExpressionList<Select>
	{
		public GroupByClause()
			: base()
		{
		}

		public GroupByClause(GroupByClause groupBy, Select select)
			: base(groupBy, select)
		{ 
		}

		public virtual GroupByClause Add(Select select)
		{
			return new GroupByClause(this, select);
		}

		public virtual GroupByClause Add(Field field)
		{
			return new GroupByClause(this, new Select(field));
		}
	}
}
