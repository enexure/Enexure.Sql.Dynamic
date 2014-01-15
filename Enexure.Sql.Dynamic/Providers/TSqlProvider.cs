using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Enexure.Sql.Dynamic.Queries;

namespace Enexure.Sql.Dynamic.Providers
{
	public class TSqlProvider
	{
		private class Provider
		{
			private readonly StringBuilder builder;
			private readonly Dictionary<Type, Action<object>> mappings;
			private readonly Dictionary<Constant, int> constants;

			private readonly List<SqlParameter> parameters;
			private int idCounter;

			public Provider(Query query)
			{
				parameters = new List<SqlParameter>();
				constants = new Dictionary<Constant, int>();

				builder = new StringBuilder();

				mappings = new Dictionary<Type, Action<object>>() {
					// Clauses
					{ typeof(SelectClause), x => Expand((Clause)x) },
					{ typeof(WhereClause), x => Expand((Clause)x) },
					{ typeof(FromClause), x => Expand((FromClause)x) },
					{ typeof(GroupByClause), x => Expand((GroupByClause)x) },
					{ typeof(OrderByClause), x => Expand((OrderByClause)x) },

					{ typeof(Query), x => Expand((Query)x) },
					{ typeof(OrderByItem), x => Expand((OrderByItem)x) },
					{ typeof(Table), x => Expand((Table)x) },
					{ typeof(TableSource), x => Expand((TableSource)x) },
					{ typeof(DerivedTable), x => Expand((DerivedTable)x) },
					{ typeof(SelectList), x => Expand((SelectList)x) },
					{ typeof(Select), x => Expand((Select)x) },
					{ typeof(Star), x => Expand((Star)x) },
					{ typeof(Field), x => Expand((Field)x) },
					{ typeof(Equality), x => Expand((Equality)x) },
					{ typeof(Constant), x => Expand((Constant)x) },
					{ typeof(Conjunction), x => Expand((Conjunction)x) },
					{ typeof(Disjunction), x => Expand((Disjunction)x) },
					{ typeof(JoinList), x => Expand((JoinList)x) },
					{ typeof(Count), x => Expand((Function)x) },
					{ typeof(Sum), x => Expand((Function)x) },
					{ typeof(Function), x => Expand((Function)x) },
					{ typeof(InValues), x => Expand((InValues)x) },
					{ typeof(Concatenation), x => Expand((Concatenation)x) },
					{ typeof(Skip), x => Expand((Skip)x) },
					{ typeof(Take), x => Expand((Take)x) },
					{ typeof(Not), x => Expand((Not)x) },
					{ typeof(IsNull), x => Expand((IsNull)x) },
				};

				Expand(query);
			}

			private void ExpandExpression(object part)
			{
				if (part == null) {
					return;
				}

				var type = part.GetType();
				Action<object> expander;
				try {
					expander = mappings[type];
				} catch (Exception) {
					throw new Exception(string.Format("Could not expand {0}", type.Name));
				}
				expander.Invoke(part);
			}
			
			private void Expand(Query query)
			{
				ExpandExpression(query.SelectClause);
				ExpandExpression(query.FromClause);
				ExpandExpression(query.WhereClause);
				ExpandExpression(query.GroupByClause);
				ExpandExpression(query.OrderByClause);
				ExpandExpression(query.SkipClause);
				ExpandExpression(query.TakeClause);
			}

			private void Expand(Clause clause)
			{
				if (!clause.ClauseList.IsEmpty) {
					if (clause.ClauseName != "select") {
						builder.AppendLine();
					}
					builder.Append(clause.ClauseName).Append(" ");

					ExpandExpression(clause.ClauseList);
				}
			}

			private void Expand(FromClause clause)
			{
				builder.AppendLine();
				builder.Append(clause.ClauseName).Append(" ");
				ExpandExpression(clause.DataSource);

				if (!clause.ClauseList.IsEmpty) {
					builder.AppendLine();
					ExpandExpression(clause.ClauseList);
				}
			}

			// ReSharper disable once ParameterTypeCanBeEnumerable.Local
			private void Expand(JoinList joins)
			{
				var joinTypes = new Dictionary<JoinType, string>() {
					{ JoinType.Inner, "inner" },
					{ JoinType.LeftOuter, "left outer" },
					{ JoinType.RightOuter, "right outer" },
					{ JoinType.FullOuter, "full outer" },
					{ JoinType.Cross, "cross" },
				};

				var head = true;
				foreach (var join in joins) {
					if (head) { head = false; } else { builder.AppendLine(); }

					if (join.JoinType != JoinType.Inner) {
						builder.Append(joinTypes[join.JoinType]).Append(" ");
					}

					builder.Append("join ");
					ExpandExpression(join.Source);
					builder.Append(" on ");
					ExpandExpression(join.Expression);
				}
			}

			private void Expand(Table table)
			{
				builder.AppendFormat("[{0}]", table.Name);
			}

			private void Expand(TableSource tableSource)
			{
				ExpandExpression(tableSource.Table);

				if (!string.IsNullOrWhiteSpace(tableSource.Alias)) {
					builder.AppendFormat(" [{0}]", tableSource.Alias);
				}
			}

			private void Expand(DerivedTable derivedTable)
			{
				builder.AppendFormat("(");
				ExpandExpression(derivedTable.Query);
				builder.AppendFormat(")");
				builder.AppendFormat(" [{0}]", derivedTable.Alias);
			}

			private void Expand(Field field)
			{
				var source = field.TabularDataSource.Alias ?? ((TableSource)field.TabularDataSource).Table.Name;

				if (field.Name == "*") {
					builder.AppendFormat("[{0}].*", source);
				} else {
					builder.AppendFormat("[{0}].[{1}]", source, field.Name);
				}
			}

			private void Expand(Equality equalityExpression)
			{
				ExpandExpression(equalityExpression.ExpressionLeft);
				builder.Append(" = ");
				ExpandExpression(equalityExpression.ExpressionRight);
			}

			private void Expand(Star star)
			{
				builder.Append("*");
			}

			private void Expand(SelectList selectList)
			{
				var head = true;
				foreach (var selectExpression in selectList) {
					if (head) { head = false; } else { builder.Append(", "); }
					ExpandExpression(selectExpression);
				}
			}

			private void Expand(Constant constantExpression)
			{
				string prexif = "p";

				var id = 0;
				if (!constants.TryGetValue(constantExpression, out id)) {
					id = idCounter++;

					var paramName = prexif + id;
					var value = constantExpression.Value;
					parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
					constants.Add(constantExpression, id);
				}

				builder.Append("@" + prexif + id);
			}

			private void Expand(Select selectExpression)
			{
				ExpandExpression(selectExpression.Expression);

				if (!string.IsNullOrWhiteSpace(selectExpression.Alias)) {
					builder.AppendFormat(" as {0}", selectExpression.Alias);
				}
			}

			private void Expand(Concatenation concatenation)
			{
				var head = true;
				foreach (var item in concatenation.Expressions) {
					if (head) { head = false; } else { builder.Append(" + "); }
					ExpandExpression(item);
				}
			}

			private void Expand(Conjunction conjunction)
			{
				var head = true;
				foreach (var item in conjunction) {
					if (head) { head = false; } else { builder.AppendLine().Append("and "); }
					ExpandExpression(item);
				}
			}

			private void Expand(Disjunction disjunction)
			{
				var head = true;
				foreach (var item in disjunction) {
					if (head) { head = false; } else { builder.AppendLine().Append("or "); }
					ExpandExpression(item);
				}
			}

			private void Expand(GroupByClause groupByClause)
			{
				if (!groupByClause.IsEmpty) {
					builder.AppendLine();
					builder.Append("group by ");

					var head = true;
					foreach (var item in groupByClause) {
						if (head) { head = false; } else { builder.Append(", "); }
						ExpandExpression(item);
					}
				}
			}

			private void Expand(OrderByClause orderByClause)
			{
				if (!orderByClause.IsEmpty) {
					builder.AppendLine();
					builder.Append("order by ");

					var head = true;
					foreach (var item in orderByClause) {
						if (head) { head = false; } else { builder.Append(", "); }
						ExpandExpression(item);
					}
				}
			}

			private void Expand(OrderByItem orderByItem)
			{
				ExpandExpression(orderByItem.Select);
				if (orderByItem.ExplicitOrder) {
					builder.Append(" ").Append(orderByItem.Order == Order.Ascending ? "asc" : "desc");
				}
			}


			private void Expand(Function function)
			{
				builder.Append(function.FunctionName);
				builder.Append("(");
				ExpandExpression(function.Expression);
				builder.Append(")");
			}

			private void Expand(Not not)
			{
				var isNull = not.Expression as IsNull;
				if (isNull == null) {
					builder.Append("not (");
					ExpandExpression(not.Expression);
					builder.Append(")");
				} else {
					ExpandExpression(isNull.Expression);
					builder.Append(" is not null");
				}
			}

			private void Expand(IsNull isNull)
			{
				ExpandExpression(isNull.Expression);
				builder.Append(" is null");
			}

			private void Expand(InValues inValues)
			{
				ExpandExpression(inValues.Expression);
				builder.Append(" in (");

				var head = true;
				foreach (var item in inValues.Values) {
					if (head) { head = false; } else { builder.Append(", "); }
					ExpandExpression(Expression.Const(item));
				}

				builder.Append(")");
			}

			private void Expand(Skip skip)
			{
				builder.AppendLine().Append("offset ");
				ExpandExpression(new Constant(skip.Rows));
				builder.Append(" rows");
			}

			private void Expand(Take take)
			{
				builder.AppendLine().Append("fetch next ");
				ExpandExpression(new Constant(take.Rows)); // Could be TabularDataSource
				builder.Append(" rows only");
			}

			public IDbCommand GetCommand()
			{
				var command = new SqlCommand() {
					CommandText = GetSqlString(),
					CommandType = CommandType.Text,
				};

				command.Parameters.AddRange(parameters.ToArray());
				return command;
			}

			public string GetSqlString()
			{
				return builder.ToString();
			}
		}

		public static IDbCommand GetCommand(Query query)
		{
			return new Provider(query).GetCommand();
		}

		public static string GetSqlString(Query query)
		{
			return new Provider(query).GetSqlString();
		}

	}
}
