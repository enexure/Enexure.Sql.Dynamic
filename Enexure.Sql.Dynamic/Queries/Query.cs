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
		//Select Clause
		private readonly SelectClause selectClause;

		// From Clause
		private readonly FromClause fromClause;
		
		// Where Clause
		private readonly WhereClause whereClause;
		
		//GroupBy Clause
		private readonly GroupByClause groupByClause;
		
		// Having Clause

		// OrderBy Clause
		private readonly OrderByClause orderByClause;
		
		// Extra
		private readonly Skip skip;
		private readonly Take take;

		public Query(TabularDataSource dataSource)
		{
			fromClause = new FromClause(dataSource);
			selectClause = new SelectClause();
			whereClause = new WhereClause();
			groupByClause = new GroupByClause();
			orderByClause = new OrderByClause();
		}

		private Query(Query query, SelectClause selectClause)
			: this(query)
		{
			this.selectClause = selectClause;
		}

		private Query(Query query, WhereClause whereClause)
			: this(query)
		{
			this.whereClause = whereClause;
		}

		private Query(Query query, FromClause fromClause)
			: this(query)
		{
			this.fromClause = fromClause;
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
			selectClause = query.selectClause;
			groupByClause = query.groupByClause;
			orderByClause = query.orderByClause;
			skip = query.skip;
			take = query.take;
		}

		public FromClause FromClause
		{
			get { return fromClause; }
		}
		
		public SelectClause SelectClause
		{
			get { return selectClause; }
		}

		public WhereClause WhereClause
		{
			get { return whereClause; }
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
			return new Query(this, new FromClause(fromClause, fromClause.List.Add(new Join(source, expression))));
		}

		public Query Join(JoinType joinType, TableSource source, IBoolean expression)
		{
			return new Query(this, new FromClause(fromClause, fromClause.List.Add(new Join(joinType, source, expression))));
		}

		public Query Where(IBoolean expression)
		{
			return new Query(this, new WhereClause(WhereClause.List.Add(expression)));
		}

		public Query Select(IEnumerable<Expression> expressions)
		{
			return new Query(this, new SelectClause(selectClause.List.Add(expressions)));
		}

		public Query Select(params Expression[] expressions)
		{
			return new Query(this, new SelectClause(selectClause.List.Add(expressions)));
		}

		public Query Select(Select selectExpression)
		{
			return new Query(this, new SelectClause(selectClause.List.Add(selectExpression)));
		}

		public Query Select(Field field)
		{
			return new Query(this, new SelectClause(selectClause.List.Add(field)));
		}

		public Query SelectAll()
		{
			return new Query(this, new SelectClause(selectClause.List.Add(new Star())));
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
