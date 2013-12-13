using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Enexure.Sql.Dynamic.Helpers;

namespace Enexure.Sql.Dynamic.Providers
{
	public class TSqlProvider
	{
		private class Provider
		{
			private readonly StringBuilder builder;
			private readonly Dictionary<Type, Action<Expression>> mappings;

			public Provider(Query query)
			{
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
				};

				Expand(query);
			}

			private void Expand(Expression expression)
			{
				var type = expression.GetType();
				try {
					mappings[type].Invoke(expression);
				} catch (Exception) {

					throw new Exception(string.Format("Could not expand {0}", type.Name));
				}
			}
			

			private void Expand(Query query)
			{
				builder.Append("select ");
				Expand(query.SelectList);
				builder.AppendLine();

				builder.Append("from ");
				Expand(query.FromClause);
				builder.AppendLine();

				if (!query.WhereClause.IsEmpty) {
					Expand(query.Joins);
				}

				if (!query.WhereClause.IsEmpty) {
					builder.Append("where ");
					Expand(query.FromClause);
					builder.AppendLine();
				}

				
			}

			private void Expand(IEnumerable<Join> tableSource)
			{
				foreach (var join in tableSource) {
					builder.AppendFormat("join ");
					Expand(join.Source);
					builder.AppendFormat(" on ");
					Expand(join.Expression);
					builder.AppendLine();
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

			private void Expand(SelectExpression selectExpression)
			{
				Expand(selectExpression.Expression);

				if (!string.IsNullOrWhiteSpace(selectExpression.Alias)) {
					builder.AppendFormat(" as {0}", selectExpression.Alias);
				}
			}

			public override string ToString()
			{
				return builder.ToString();
			}
		}

		public static IDbCommand GetCommand(Query query)
		{
			throw new NotImplementedException();
		}

		public static string GetSqlString(Query query)
		{
			return new Provider(query).ToString();
		}

	}
}
