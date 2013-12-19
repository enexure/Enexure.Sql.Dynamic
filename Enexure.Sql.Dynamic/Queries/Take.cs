using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Take : Expression
	{
		private readonly int rows;

		public Take()
		{
		}

		public Take(int rows)
		{
			this.rows = rows;
		}

		public int Rows
		{
			get { return rows; }
		}
	}
}
