using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public sealed class Query : Expression
	{
		private readonly SelectList selectList;
		private readonly TabularDataSource fromClause;
		private readonly JoinList joins;
		private readonly Conjunction whereClause;
		private readonly GroupByClause groupByClause;

		public Query(TabularDataSource fromClause)
		{
			this.fromClause = fromClause;

			selectList = new SelectList();
			whereClause = new Conjunction();
			joins = new JoinList();
			groupByClause = new GroupByClause();
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

		private Query(Query query, JoinList joins)
			: this(query)
		{
			this.joins = joins;
		}

		private Query(Query query, GroupByClause groupByClause)
			: this(query)
		{
			this.groupByClause = groupByClause;
		}

		private Query(Query query)
		{
			fromClause = query.fromClause;
			whereClause = query.whereClause;
			joins = query.joins;
			selectList = query.selectList;
			groupByClause = query.groupByClause;
		}

		public SelectList SelectList
		{
			get { return selectList; }
		}

		public TabularDataSource FromClause
		{
			get { return fromClause; }
		}

		public Conjunction WhereClause
		{
			get { return whereClause; }
		}

		public JoinList Joins
		{
			get { return joins; }
		}

		public GroupByClause GroupByClause
		{
			get { return groupByClause; }
		}

		public static Query From(Table table)
		{
			return new Query(new TableSource(table));
		}

		public static Query From(TabularDataSource tabularDataSource)
		{
			return new Query(tabularDataSource);
		}

		public Query Join(TableSource source, Boolean expression)
		{
			return new Query(this, new Join(source, expression));
		}

		public Query Join(JoinType joinType, TableSource source, Boolean expression)
		{
			return new Query(this, new Join(joinType, source, expression));
		}

		public Query Where(Boolean expression)
		{
			return new Query(this, (WhereClause == null) ? new Conjunction(expression) : whereClause.Add(expression));
		}

		public Query Select(IEnumerable<Expression> expressions)
		{
			return new Query(this, selectList.Add(expressions));
		}

		public Query Select(params Expression[] expressions)
		{
			return new Query(this, selectList.Add(expressions));
		}

		public Query Select(Select selectExpression)
		{
			return new Query(this, selectList.Add(selectExpression));
		}

		public Query Select(Field field)
		{
			return new Query(this, selectList.Add(field));
		}

		public Query SelectAll()
		{
			return new Query(this, selectList.Add(new Star()));
		}

		public Query GroupBy(Field field)
		{
			return new Query(this, groupByClause.Add(field));
		}

		public Query GroupBy(Select selectExpression)
		{
			return new Query(this, groupByClause.Add(selectExpression));
		}

		public DerivedTable As(string alias)
		{
			return new DerivedTable(this, alias);
		}
	}
}
