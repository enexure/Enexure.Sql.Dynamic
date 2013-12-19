using System;
using Enexure.Sql.Dynamic.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enexure.Sql.Dynamic.Providers;

namespace Enexure.Sql.Dynamic.Tests
{
	/// <summary>
	/// Summary description for DerivedTablesTest
	/// </summary>
	[TestClass]
	public class InTest
	{
		[TestMethod]
		public void InValues()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Where(Expression.In(table.Field("Name"), new [] { "Bob", "Jack" }))
				.Select(Field.All());

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select *" + Environment.NewLine +
				"from [Table]" + Environment.NewLine + 
				"where [Table].[Name] in (@p0, @p1)";

			Assert.AreEqual(expected, sql);
		}

	}
}
