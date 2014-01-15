using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public interface IExpression
	{
		Select As(string alias);

		Select AsSelf();

		Function WithFunc(string function);
	}
}
