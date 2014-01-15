using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class WhereClause : Clause<Conjunction>
	{
		public WhereClause()
			: base(Conjunction.Empty)
		{
		}

		public WhereClause(Conjunction list)
			: base(list)
		{
		}

		public override string ClauseName
		{
			get { return "where"; }
		}

	}
}
