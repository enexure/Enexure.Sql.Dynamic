using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic.Queries
{
	public class MustHaveAtLeastOneException : Exception
	{
		public MustHaveAtLeastOneException(string message)
			: base(message)
		{
		}
	}
}
