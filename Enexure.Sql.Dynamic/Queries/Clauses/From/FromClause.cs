using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class FromClause : Clause<JoinList>
	{
		private readonly TabularDataSource dataSource;

		public FromClause(TabularDataSource dataSource)
			: base(JoinList.Empty)
		{
			this.dataSource = dataSource;
		}

		public FromClause(TabularDataSource dataSource, JoinList list)
			: base(list)
		{
			this.dataSource = dataSource;
		}

		public FromClause(FromClause fromClause, JoinList list)
			: base(list)
		{
			this.dataSource = fromClause.dataSource;
		}

		public TabularDataSource DataSource {
			get { return dataSource; }
		}

		public IEnumerable<TabularDataSource> Tables
		{
			get
			{
				yield return dataSource;
				foreach (var table in List.Select(x => x.Source)) {
					yield return table;
				}
			}
		}

		public override string ClauseName
		{
			get { return "from"; }
		}

	}
}
