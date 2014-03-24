using System;
using Enexure.Sql.Dynamic.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enexure.Sql.Dynamic.Tests
{
	[TestClass]
	public class QueryTests
	{
		[TestMethod]
		public void Constructing_Query_Should_Not_Throw()
		{
			var table = new Table("TableName").As("Alias");
			var id = table.Field("Id");
			
			Query
				.From(table)
				.Where(Expression.Eq(id, 1))
				.Select(table.Field("*"));
		}
	}
}
