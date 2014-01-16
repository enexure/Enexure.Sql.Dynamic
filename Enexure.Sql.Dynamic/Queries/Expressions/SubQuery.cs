using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class SubQuery : Expression
	{
		private readonly Query query;

		public SubQuery(Query query)
		{
			this.query = query;
		}

		public Query Query
		{
			get { return query; }
		}
	}
}
