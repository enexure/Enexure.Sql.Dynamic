Enexure.Sql.Dynamic (alpha)
===

An Immutable Dynamic Sql Generation Library

#How to use 

All queryies start by creating a `Query` from a `TabularDataSource` this could be a table to a derived table (subquery).

	var tableA = new Table("TableA").As("a");
	var tableB = new Table("TableB").As("b");

	var query = Query
		.From(tableA)
		.Join(tableB, Expression.Eq(tableA.Field("Id"), tableB.Field("Fk")))
		.Where(Expression.Eq(tableA.Field("Id"), Expression.Const(1)))
		.Select(tableA.Field("Id"), tableB.All());

Once you've constructed your query you need to use a provider to generate the DbCommand and SQL. 

	// Get the DbCommand
	var command = TSqlProvider.GetCommand(query)

	// Get the just SQL, great for debugging 
	var sql = TSqlProvider.GetSqlString(query);

The resulting SQL for the query above looks like the following.

	select [a].[Id], [b].*
	from [TableA] [a]
	join [TableB] [b] on [a].[Id] = [b].[Fk]
	where [a].[Id] = @p0