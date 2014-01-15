using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class SelectClause : Clause<SelectList>
	{
		public SelectClause()
			: base(SelectList.Empty)
		{
		}

		public SelectClause(SelectList list)
			: base(list)
		{
		}

		public override string ClauseName
		{
			get { return "select"; }
		}

	}
}
