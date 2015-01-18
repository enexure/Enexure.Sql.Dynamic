Enexure.Sql.Dynamic
===================
[![Build status](https://ci.appveyor.com/api/projects/status/1ev9rhfabj5y4jli/branch/master?svg=true)](https://ci.appveyor.com/project/Daniel45729/enexure-sql-dynamic/branch/master)

Dynamic Sql Generation Library

##How to use 

All queryies start by creating a `Query` from a `TabularDataSource` this could be a table to a derived table (subquery).

	var tableA = new Table("TableA").As("a");
	var tableB = new Table("TableB").As("b");

	var query = Query
		.From(tableA)
		.Join(tableB, Expression.Eq(tableA.Field("Id"), tableB.Field("Fk")))
		.Where(Expression.Eq(tableA.Field("Id"), Expression.Const(1)))
		.Select(tableA.Field("Id"), tableB.All());

The entire query api is immutable, which means copying and reusing parts of any query is easy.

	var people = new Table("People").As("p");

	var queryBase = Query.From(people);

	var countQuery = queryBase.Select(Field.All.Count())
	var resultsQuery = queryBase.Select(people.All());

## Providers

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
