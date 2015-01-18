using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class InSubQuery : In
	{
		private readonly SubQuery subQuery;

		public InSubQuery(IExpression expression, SubQuery subQuery)
			: base(expression)
		{
			this.subQuery = subQuery;
		}

		public SubQuery SubQuery
		{
			get { return subQuery; }
		}
	}
}
