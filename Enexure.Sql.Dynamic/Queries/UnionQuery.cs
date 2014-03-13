using System;
using System.Collections.Generic;

namespace Enexure.Sql.Dynamic.Queries
{
	public enum UnionType
	{
		Distinct,
		All
	}

	public class UnionQuery : BaseQuery
	{
		private readonly IEnumerable<Query> queries;
		private readonly UnionType unionType;

		public UnionQuery(IEnumerable<Query> queries, UnionType unionType)
		{
			this.queries = queries;
			this.unionType = unionType;
		}

		public IEnumerable<Query> Queries
		{
			get { return queries; }
		}

		public UnionType UnionType
		{
			get { return unionType; }
		}
	}
}
