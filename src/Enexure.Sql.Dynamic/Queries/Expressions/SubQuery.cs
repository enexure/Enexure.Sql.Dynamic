using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class SubQuery : Expression
	{
		private readonly BaseQuery query;

		public SubQuery(BaseQuery query)
		{
			this.query = query;
		}

		public BaseQuery Query
		{
			get { return query; }
		}
	}
}
