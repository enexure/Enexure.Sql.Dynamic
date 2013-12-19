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
	public class GroupByTest
	{
		[TestMethod]
		public void GroupByOneColumn()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Select(table.Field("Name"))
				.GroupBy(table.Field("Name"));

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [Table].[Name]" + Environment.NewLine +
				"from [Table]" + Environment.NewLine +
				"group by [Table].[Name]";

			Assert.AreEqual(expected, sql);
		}

		[TestMethod]
		public void GroupByMoreThanOneColumn()
		{
			var table = new Table("Table");

			var query = Query
				.From(table)
				.Select(table.Field("Name"))
				.GroupBy(table.Field("Name"))
				.GroupBy(table.Field("Size"));

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [Table].[Name]" + Environment.NewLine +
				"from [Table]" + Environment.NewLine +
				"group by [Table].[Name], [Table].[Size]";

			Assert.AreEqual(expected, sql);
		}
	}
}
