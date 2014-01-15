using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class Clause<T>
	{
		public abstract string ClauseName { get; }

		public ExpressionList<T> ExpressionList { get; set; }
	}
}
