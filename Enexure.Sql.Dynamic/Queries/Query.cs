using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public sealed class Query //: IExpression Add Method to SubQuery
	{
		private readonly SelectList selectList;
		private readonly TabularDataSource fromClause;
		private readonly JoinList joins;
		private readonly Conjunction whereClause;
		private readonly GroupByClause groupByClause;
		private readonly OrderByClause orderByClause;
		private readonly Skip skip;
		private readonly Take take;

		public Query(TabularDataSource fromClause)
		{
			this.fromClause = fromClause;

			selectList = new SelectList();
			whereClause = new Conjunction();
			joins = new JoinList();
			groupByClause = new GroupByClause();
			orderByClause = new OrderByClause();
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

		private Query(Query query, OrderByClause orderByClause)
			: this(query)
		{
			this.orderByClause = orderByClause;
		}

		private Query(Query query, Skip skip)
			: this(query)
		{
			this.skip = skip;
		}

		private Query(Query query, Take take)
			: this(query)
		{
			this.take = take;
		}

		private Query(Query query)
		{
			fromClause = query.fromClause;
			whereClause = query.whereClause;
			joins = query.joins;
			selectList = query.selectList;
			groupByClause = query.groupByClause;
			orderByClause = query.orderByClause;
			skip = query.skip;
			take = query.take;
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

		public OrderByClause OrderByClause
		{
			get { return orderByClause; }
		}

		public Skip SkipClause
		{
			get { return skip; }
		}

		public Take TakeClause
		{
			get { return take; }
		}

		public IEnumerable<TabularDataSource> Tables
		{
			get
			{
				yield return fromClause;
				foreach (var table in Joins.Select(x => x.Source)) {
					yield return table;
				}
			}
		}

		public static Query From(Table table)
		{
			return new Query(new TableSource(table));
		}

		public static Query From(TabularDataSource tabularDataSource)
		{
			return new Query(tabularDataSource);
		}

		public Query Join(TableSource source, IBoolean expression)
		{
			return new Query(this, new Join(source, expression));
		}

		public Query Join(JoinType joinType, TableSource source, IBoolean expression)
		{
			return new Query(this, new Join(joinType, source, expression));
		}

		public Query Where(IBoolean expression)
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

		public Query OrderBy(Field field)
		{
			return new Query(this, orderByClause.Add(new OrderByItem(field)));
		}

		public Query OrderBy(Select selectExpression)
		{
			return new Query(this, orderByClause.Add(new OrderByItem(selectExpression)));
		}

		public Query OrderBy(Field field, Order order)
		{
			return new Query(this, orderByClause.Add(new OrderByItem(field, order)));
		}

		public Query OrderBy(Select selectExpression, Order order)
		{
			return new Query(this, orderByClause.Add(new OrderByItem(selectExpression, order)));
		}

		public DerivedTable As(string alias)
		{
			return new DerivedTable(this, alias);
		}

		public Query Skip(int rows)
		{
			return new Query(this, new Skip(rows));
		}

		public Query Take(int rows)
		{
			return new Query(this, new Take(rows));
		}
	}
}
