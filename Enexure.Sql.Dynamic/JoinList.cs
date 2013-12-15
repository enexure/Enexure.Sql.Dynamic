using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class JoinList : ExpressionList<Join>
	{
		public JoinList()
			: base()
		{
		}

		public JoinList(Join expression)
			: base(expression)
		{
		}

		private JoinList(JoinList list, Join expression)
			: base(list, expression)
		{
		}

		public JoinList Add(Join join)
		{
			return new JoinList(this, join);
		}
	}
}
