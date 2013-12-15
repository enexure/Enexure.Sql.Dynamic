using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class DerivedTable : DataSource
	{
		private readonly Query query;

		public DerivedTable(Query query, string alias)
			: base(alias)
		{
			this.query = query;
		}

		public Query Query {
			get {
				return query;
			}
		}
	}
}
