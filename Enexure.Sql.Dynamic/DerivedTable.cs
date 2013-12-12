using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class DerivedTable : DataSource
	{
		private readonly DataSource source;

		public DerivedTable(DataSource source, string alias)
			: base(alias)
		{
			this.source = source;
		}
	}
}
