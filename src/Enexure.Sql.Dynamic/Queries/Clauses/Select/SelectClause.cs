using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class SelectClause : Clause<SelectList>
	{
		private readonly bool distinct;
		private readonly int? first;

		public SelectClause()
			: base(SelectList.Empty)
		{
		}

		public SelectClause(SelectList list)
			: base(list)
		{
		}

		public SelectClause(SelectClause clause, int first)
			: base(clause.List)
		{
			this.first = first;
		}

		public SelectClause(SelectClause clause, bool distinct)
			: base(clause.List)
		{
			this.distinct = distinct;
		}

		public bool Distinct
		{
			get { return distinct; }
		}

		public int? First
		{
			get { return first; }
		}

		public override string ClauseName
		{
			get { return "select"; }
		}

	}
}
