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
			private readonly Dictionary<Type, Action<Expression>> mappings;
			private readonly Dictionary<Constant, int> constants;

			private readonly List<SqlParameter> parameters;
			private int idCounter;

			public Provider(Query query)
			{
				parameters = new List<SqlParameter>();
				constants = new Dictionary<Constant, int>();

				builder = new StringBuilder();

				mappings = new Dictionary<Type, Action<Expression>>() {
					{ typeof(Query), x => Expand((Query)x) },
					{ typeof(Table), x => Expand((Table)x) },
					{ typeof(TableSource), x => Expand((TableSource)x) },
					{ typeof(DerivedTable), x => Expand((DerivedTable)x) },
					{ typeof(Star), x => Expand((Star)x) },
					{ typeof(SelectList), x => Expand((SelectList)x) },
					{ typeof(Select), x => Expand((Select)x) },
					{ typeof(Field), x => Expand((Field)x) },
					{ typeof(Equality), x => Expand((Equality)x) },
					{ typeof(Constant), x => Expand((Constant)x) },
					{ typeof(Conjunction), x => Expand((Conjunction)x) },
					{ typeof(JoinList), x => Expand((JoinList)x) },
					{ typeof(GroupByClause), x => Expand((GroupByClause)x) },
					{ typeof(Count), x => Expand((Function)x) },
					{ typeof(Sum), x => Expand((Function)x) },
					{ typeof(Function), x => Expand((Function)x) },
					{ typeof(InValues), x => Expand((InValues)x) },
					{ typeof(Concatenation), x => Expand((Concatenation)x) },
				};

				Expand(query);
			}

			private void ExpandExpression(Expression expression)
			{
				var type = expression.GetType();
				Action<Expression> expander;
				try {
					expander = mappings[type];
				} catch (Exception) {
					throw new Exception(string.Format("Could not expand {0}", type.Name));
				}
				expander.Invoke(expression);
			}
			

			private void Expand(Query query)
			{
				builder.Append("select ");
				ExpandExpression(query.SelectList);
				builder.AppendLine();

				builder.Append("from ");
				ExpandExpression(query.FromClause);

				if (!query.Joins.IsEmpty) {
					builder.AppendLine();
					ExpandExpression(query.Joins);
				}

				if (!query.WhereClause.IsEmpty) {
					builder.AppendLine();
					builder.Append("where ");
					ExpandExpression(query.WhereClause);
				}

				ExpandExpression(query.GroupByClause);
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

			private void Expand(Function function)
			{
				builder.Append(function.FunctionName);
				builder.Append("(");
				ExpandExpression(function.Expression);
				builder.Append(")");
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
