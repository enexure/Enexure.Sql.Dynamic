using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class Clause<T> : Clause
		where T : ExpressionList
	{
		private readonly T list;

		protected Clause(T list)
		{
			this.list = list;
		}

		public T List
		{
			get { return list; }
		}

		public override ExpressionList ClauseList
		{
			get { return list; }
		}
	}

	public abstract class Clause
	{
		public abstract string ClauseName { get; }

		public abstract ExpressionList ClauseList { get; }
	}
}
