using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class Field : Expression
	{
		private readonly Table table;
		private readonly string name;

		public Field(Table table, string name)
		{
			this.table = table;
			this.name = name;
		}

		public SelectExpression As(string alias)
		{
			return new SelectExpression(this, alias);
		}
	}
}
