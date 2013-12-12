using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enexure.Sql.Dynamic.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var tableA = new Table("TableA");
			var tableB = new Table("TableB");

			var sql = Query.From(tableA.As("a"))
				.Join(tableB.As("b"), Expression.Eq(tableA.Field("Id"), tableB.Field("Fk")))
				.Where(Expression.Eq(tableA.Field("Id"), Expression.Const(1)))
				.Select(tableA.Field("Id"), tableB.All())
				.ToString();

			var expected = 
				"select a.Id, b.* " + Environment.NewLine +
				"from TableA a" + Environment.NewLine +
				"join TableB b on a.Id = b.Fk" + Environment.NewLine +
				"where a.Id = @a";
		}
	}
}
