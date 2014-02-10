using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Equal : Operator
	{
		public Equal(IExpression expressionLeft, IExpression expressionRight)
			: base(expressionLeft, expressionRight)
		{
		}

		public override string OperatorSymbol
		{
			get { return "="; }
		}
	}
}
