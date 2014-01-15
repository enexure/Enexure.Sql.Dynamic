using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public abstract class Aggregate : Function
	{
		protected Aggregate(string function, IExpression expression)
			: base(function, expression)
		{
		}
	}
}
