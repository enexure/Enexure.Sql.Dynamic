using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Data.SqlClient;

namespace Enexure.Sql.Dynamic.Providers
{
	public class TSqlProvider
	{
		private class Provider
		{
			private readonly StringBuilder builder;
			private readonly Dictionary<Type, Action<Expression>> mappings;
			private readonly Dictionary<ConstantExpression, int> constants;

			//IDbParameterCollection
			private readonly List<SqlParameter> parameters;
			private int idCounter;

			public Provider(Query query)
			{
				parameters = new List<SqlParameter>();
				constants = new Dictionary<ConstantExpression, int>();

				builder = new StringBuilder();

				mappings = new Dictionary<Type, Action<Expression>>() {
					{ typeof(Query), x => Expand((Query)x) },
					{ typeof(Table), x => Expand((Table)x) },
					{ typeof(TableSource), x => Expand((TableSource)x) },
					{ typeof(DerivedTable), x => Expand((DerivedTable)x) },
					{ typeof(SelectList), x => Expand((SelectList)x) },
					{ typeof(SelectExpression), x => Expand((SelectExpression)x) },
					{ typeof(Field), x => Expand((Field)x) },
					{ typeof(EqualityExpression), x => Expand((EqualityExpression)x) },
					{ typeof(ConstantExpression), x => Expand((ConstantExpression)x) },
				};

				Expand(query);
			}

			private void Expand(Expression expression)
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
				Expand(query.SelectList);
				builder.AppendLine();

				builder.Append("from ");
				Expand(query.FromClause);

				if (query.Joins.Any()) {
					builder.AppendLine();
					Expand(query.Joins);
				}

				if (!query.WhereClause.IsEmpty) {
					builder.AppendLine();
					builder.Append("where ");
					Expand(query.WhereClause);
				}

				
			}

			private void Expand(IEnumerable<Join> tableSource)
			{
				var head = true;
				foreach (var join in tableSource) {
					if (head) { head = false; } else { builder.AppendLine(); }

					builder.AppendFormat("join ");
					Expand(join.Source);
					builder.AppendFormat(" on ");
					Expand(join.Expression);
				}
			}

			private void Expand(Table table)
			{
				builder.AppendFormat("[{0}]", table.Name);
			}

			private void Expand(TableSource tableSource)
			{
				builder.AppendFormat("[{0}] [{1}]", tableSource.Table.Name, tableSource.Alias);
			}

			private void Expand(DerivedTable derivedTable)
			{
				throw new NotImplementedException();

				//Expand(derivedTable.);
				//builder.AppendFormat(" [{0}]", derivedTable.Alias);
			}

			private void Expand(Field field)
			{
				var source = field.DataSource.Alias ?? ((TableSource)field.DataSource).Table.Name;

				if (field.Name == "*") {
					builder.AppendFormat("[{0}].*", source);
				} else {
					builder.AppendFormat("[{0}].[{1}]", source, field.Name);
				}
			}

			private void Expand(EqualityExpression equalityExpression)
			{
				Expand(equalityExpression.ExpressionLeft);
				builder.Append(" = ");
				Expand(equalityExpression.ExpressionRight);
			}

			private void Expand(SelectList selectList)
			{
				var head = true;
				foreach (var selectExpression in selectList) {
					if (head) { head = false; } else { builder.Append(", "); }
					Expand(selectExpression);
				}
			}

			private void Expand(ConstantExpression constantExpression)
			{
				var id = 0;
				if (!constants.TryGetValue(constantExpression, out id)) {
					id = idCounter++;
				}

				var paramName = "p" + id;
				var value = constantExpression.Value;

				parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
				builder.Append("@" + paramName);
			}

			private void Expand(SelectExpression selectExpression)
			{
				Expand(selectExpression.Expression);

				if (!string.IsNullOrWhiteSpace(selectExpression.Alias)) {
					builder.AppendFormat(" as {0}", selectExpression.Alias);
				}
			}

			private void Expand(Conjunction conjunction)
			{
				var head = true;
				foreach (var item in conjunction) {
					if (head) { head = false; } else { builder.Append(" and "); }
					Expand(item);
				}
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
