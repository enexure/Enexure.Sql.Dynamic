using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public abstract class DataSource : Expression
	{
		private readonly string alias;

		protected DataSource(string alias)
		{
			this.alias = alias;
		}

		public string Alias
		{
			get { return alias; }
		}
	}
}
