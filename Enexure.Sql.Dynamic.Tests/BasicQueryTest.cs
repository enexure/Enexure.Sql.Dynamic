using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enexure.Sql.Dynamic.Providers;

namespace Enexure.Sql.Dynamic.Tests
{
	/// <summary>
	/// Summary description for BasicQueryTest
	/// </summary>
	[TestClass]
	public class BasicQueryTest
	{
		[TestMethod]
		public void SimpleQuery()
		{
			var query = Query
				.From(new Table("TableA"))
				.SelectAll();

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select *" + Environment.NewLine +
				"from [TableA]";

			Assert.AreEqual(expected, sql);
		}
	}
}
