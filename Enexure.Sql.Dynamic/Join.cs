using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enexure.Sql.Dynamic
{
	public class Join : Expression
	{
		private readonly TableSource source;
		private readonly Boolean expression;

		public Join(TableSource source, Boolean expression)
		{
			this.source = source;
			this.expression = expression;
		}

		public TableSource Source
		{
			get { return source; }
		}

		public Boolean Expression
		{
			get { return expression; }
		}
	}
}
