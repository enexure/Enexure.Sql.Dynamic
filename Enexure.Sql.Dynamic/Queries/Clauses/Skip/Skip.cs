using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Skip
	{
		private readonly int rows;

		public Skip()
		{
		}

		public Skip(int rows)
		{
			this.rows = rows;
		}

		public int Rows
		{
			get { return rows; }
		}
	}
}
