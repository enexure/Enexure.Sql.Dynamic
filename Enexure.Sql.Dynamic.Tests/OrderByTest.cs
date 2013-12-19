using System;
using System.Text;
using System.Collections.Generic;
using Enexure.Sql.Dynamic.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enexure.Sql.Dynamic.Providers;

namespace Enexure.Sql.Dynamic.Tests
{
	/// <summary>
	/// Summary description for DerivedTablesTest
	/// </summary>
	[TestClass]
	public class OrderByTest
	{
		[TestMethod]
		public void OrderByOneColumn()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Select(table.Field("Name"))
				.OrderBy(table.Field("Name"));

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [Table].[Name]" + Environment.NewLine +
				"from [Table]" + Environment.NewLine +
				"order by [Table].[Name]";

			Assert.AreEqual(expected, sql);
		}

		[TestMethod]
		public void OrderByMoreThanOneColumn()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Select(table.Field("Name"))
				.OrderBy(table.Field("Name"))
				.OrderBy(table.Field("Size"));

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [Table].[Name]" + Environment.NewLine +
				"from [Table]" + Environment.NewLine +
				"order by [Table].[Name], [Table].[Size]";

			Assert.AreEqual(expected, sql);
		}

		[TestMethod]
		public void OrderByExplicit()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Select(table.Field("Name"))
				.OrderBy(table.Field("Name"), Order.Ascending);

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [Table].[Name]" + Environment.NewLine +
				"from [Table]" + Environment.NewLine +
				"order by [Table].[Name] asc";

			Assert.AreEqual(expected, sql);
		}
	}
}
