using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enexure.Sql.Dynamic.Providers;

namespace Enexure.Sql.Dynamic.Tests
{
	/// <summary>
	/// Tests how the query handles parameters
	/// </summary>
	[TestClass]
	public class ParametersTest
	{
		[TestMethod]
		public void MultipleParameters()
		{
			var tableA = new Table("TableA").As("a");

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("A"), Expression.Const(1)))
				.Where(Expression.Eq(tableA.Field("B"), Expression.Const(2)))
				.Where(Expression.Eq(tableA.Field("C"), Expression.Const(3)))
				.Where(Expression.Eq(tableA.Field("D"), Expression.Const(4)))
				.Select(tableA.All());

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [a].*" + Environment.NewLine +
				"from [TableA] [a]" + Environment.NewLine +
				"where [a].[A] = @p0" + Environment.NewLine +
				"and [a].[B] = @p1" + Environment.NewLine +
				"and [a].[C] = @p2" + Environment.NewLine +
				"and [a].[D] = @p3";

			Assert.AreEqual(expected, sql);
		}

		[TestMethod]
		public void DifferentTypes()
		{
			var tableA = new Table("TableA").As("a");

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("A"), Expression.Const(1)))
				.Where(Expression.Eq(tableA.Field("C"), Expression.Const("Cat")))
				.Where(Expression.Eq(tableA.Field("D"), Expression.Const(DateTime.Now)))
				.Select(tableA.All());

			var cmd = TSqlProvider.GetCommand(query);

			var p1 = cmd.Parameters["p0"];

			//Assert.AreEqual(expected, sql);
		}

		[TestMethod]
		public void WorkingWithNulls()
		{
			var tableA = new Table("TableA").As("a");

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("B"), Expression.Const(null)))
				.Select(tableA.All());

			var sql = TSqlProvider.GetSqlString(query);

			var expected =
				"select [a].[Id], [b].*" + Environment.NewLine +
				"from [TableA] [a]" + Environment.NewLine +
				"join [TableB] [b] on [a].[Id] = [b].[Fk]" + Environment.NewLine +
				"where [a].[Id] = @p";

			Assert.AreEqual(expected, sql);
		}
	}
}
