using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic
{
	public class ConstantExpression : Expression
	{
		private readonly object value;

		public ConstantExpression(object value)
		{
			this.value = value;
		}
	}
}
