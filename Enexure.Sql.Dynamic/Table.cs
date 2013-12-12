using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public sealed class Table
	{
		private readonly string name;

		public Table(string name)
		{
			this.name = name;
		}

		public SelectExpression All()
		{
			return new SelectExpression(new Field(this, "*"));
		}

		public TableSource As(string alias)
		{
			return new TableSource(this, alias);
		}

		public Field Field(string name)
		{
			return new Field(this, name);
		}
	}
}
