using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class TableSource : DataSource
	{
		private readonly Table table;

		public TableSource(Table table, string alias)
			: base(alias)
		{
			this.table = table;
		}

	}
}
