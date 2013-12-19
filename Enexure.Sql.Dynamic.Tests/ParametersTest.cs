using System;
using System.Text;
using System.Collections.Generic;
using Enexure.Sql.Dynamic.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enexure.Sql.Dynamic.Providers;
using System.Data.SqlClient;

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
			var date = DateTime.Now;

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("A"), Expression.Const(1)))
				.Where(Expression.Eq(tableA.Field("C"), Expression.Const("Cat")))
				.Where(Expression.Eq(tableA.Field("D"), Expression.Const(date)))
				.Select(tableA.All());

			var cmd = TSqlProvider.GetCommand(query);

			Assert.AreEqual(3, cmd.Parameters.Count);

			var p1 = (SqlParameter)cmd.Parameters["p0"];
			var p2 = (SqlParameter)cmd.Parameters["p1"];
			var p3 = (SqlParameter)cmd.Parameters["p2"];

			Assert.AreEqual(1, p1.Value);
			Assert.AreEqual("Cat", p2.Value);
			Assert.AreEqual(date, p3.Value);
		}

		[TestMethod]
		public void SharingParameters()
		{
			var tableA = new Table("TableA").As("a");

			var constant = Expression.Const(1);

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("A"), constant))
				.Where(Expression.Eq(tableA.Field("C"), constant))
				.Where(Expression.Eq(tableA.Field("D"), constant))
				.Select(tableA.All());

			var cmd = TSqlProvider.GetCommand(query);

			Assert.AreEqual(1, cmd.Parameters.Count);

			var p1 = (SqlParameter)cmd.Parameters["p0"];

			Assert.AreEqual(1, p1.Value);
		}

		[TestMethod]
		public void WorkingWithNulls()
		{
			var tableA = new Table("TableA").As("a");

			var query = Query
				.From(tableA)
				.Where(Expression.Eq(tableA.Field("B"), Expression.Const(null)))
				.Select(tableA.All());

			var cmd = TSqlProvider.GetCommand(query);

			Assert.AreEqual(1, cmd.Parameters.Count);

			var p1 = (SqlParameter)cmd.Parameters["p0"];

			Assert.AreEqual(DBNull.Value, p1.Value);
		}
	}
}
