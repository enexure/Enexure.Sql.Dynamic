using System;

namespace Enexure.Sql.Dynamic.Queries
{
	public class DerivedTable : TabularDataSource
	{
		private readonly BaseQuery query;

		public DerivedTable(BaseQuery query, string alias)
			: base(alias)
		{
			this.query = query;
		}

		public BaseQuery Query
		{
			get {
				return query;
			}
		}
	}
}
