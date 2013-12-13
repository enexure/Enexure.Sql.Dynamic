using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public sealed class Query : Expression
	{
		private readonly SelectList selectList;
		private readonly DataSource fromClause;
		private readonly ImmutableList<Join> joins;
		private readonly Conjunction whereClause;
		// GroupByClause

		public Query(DataSource fromClause)
		{
			this.fromClause = fromClause;

			selectList = new SelectList();
			whereClause = new Conjunction();
			joins = ImmutableList<Join>.Empty;
		}

		private Query(Query query, SelectList selectList)
			: this(query)
		{
			this.selectList = selectList;
		}

		private Query(Query query, Conjunction whereClause)
			: this(query)
		{
			this.whereClause = whereClause;
		}

		private Query(Query query, Join join)
			: this(query)
		{
			this.joins = query.joins.Add(join);
		}

		private Query(Query query, ImmutableList<Join> joins)
			: this(query)
		{
			this.joins = joins;
		}

		private Query(Query query)
		{
			fromClause = query.fromClause;
			whereClause = query.whereClause;
			joins = query.joins;
			selectList = query.selectList;
		}

		public SelectList SelectList
		{
			get { return selectList; }
		}

		public DataSource FromClause
		{
			get { return fromClause; }
		}

		public Conjunction WhereClause
		{
			get { return whereClause; }
		}

		public IEnumerable<Join> Joins
		{
			get { return joins; }
		}

		public static Query From(DataSource dataSource)
		{
			return new Query(dataSource);
		}

		public Query Join(TableSource source, BooleanExpression expression)
		{
			return new Query(this, new Join(source, expression));
		}

		public Query Where(BooleanExpression expression)
		{
			return new Query(this, (WhereClause == null) ? new Conjunction(expression) : whereClause.Add(expression));
		}

		public Query Select(params Expression[] expressions)
		{
			return new Query(this, selectList.Add(expressions));
		}

		public Query Select(SelectExpression selectExpression)
		{
			return new Query(this, selectList.Add(selectExpression));
		}

		public Query Select(Field field)
		{
			return new Query(this, selectList.Add(field));
		}

	}
}
