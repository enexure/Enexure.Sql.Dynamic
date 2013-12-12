using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class DataSource
	{
		private readonly string alias;

		public DataSource(string alias)
		{
			this.alias = alias;
		}
	}
}
