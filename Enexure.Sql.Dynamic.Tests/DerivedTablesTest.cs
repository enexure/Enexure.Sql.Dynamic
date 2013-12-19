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
	public class DerivedTablesTest
	{
		[TestMethod]
		public void SingleLevel()
		{
			var query = Query
				.From(new Table("TableA"))
				.SelectAll();

			var query2 = Query
				.From(query.As("a"))
				.SelectAll();

			var sql = TSqlProvider.GetSqlString(query2);

			var expected =
				"select *" + Environment.NewLine +
				"from (select *" + Environment.NewLine +
				"from [TableA]) [a]";

			Assert.AreEqual(expected, sql);
		}
	}
}
