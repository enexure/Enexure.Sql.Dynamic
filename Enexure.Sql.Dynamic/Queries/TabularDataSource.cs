using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class TabularDataSource : Expression
	{
		private readonly string alias;

		protected TabularDataSource(string alias)
		{
			this.alias = alias;
		}

		public string Alias
		{
			get { return alias; }
		}

		public Field Field(string name)
		{
			return new Field(this, name);
		}
	}
}
