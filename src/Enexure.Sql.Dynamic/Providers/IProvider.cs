using System;
using System.Data;
using Enexure.Sql.Dynamic.Queries;

namespace Enexure.Sql.Dynamic.Providers
{
	public interface IProvider
	{
		IDbCommand GetCommand(BaseQuery query);

		string GetSqlString(BaseQuery query);
	}
}
