using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public sealed class Query
	{
		private readonly SelectList select;
		private readonly DataSource from;
		private readonly Conjunction where;

		public Query(DataSource from)
		{
			this.from = from;
		}

		public static Query From(DataSource dataSource)
		{
			return new Query(dataSource);
		}
	}
}
