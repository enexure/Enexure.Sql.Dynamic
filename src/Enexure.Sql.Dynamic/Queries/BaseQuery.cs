using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class BaseQuery
	{
		public DerivedTable As(string alias)
		{
			return new DerivedTable(this, alias);
		}

		public SubQuery AsSubQuery()
		{
			return new SubQuery(this);
		}
	}
}
