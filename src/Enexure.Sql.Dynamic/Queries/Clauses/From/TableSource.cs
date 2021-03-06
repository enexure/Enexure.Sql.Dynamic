﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class TableSource : TabularDataSource
	{
		private readonly Table table;

		public TableSource(Table table)
			: base(null)
		{
			this.table = table;
		}

		public TableSource(Table table, string alias)
			: base(alias)
		{
			this.table = table;
		}

		public Table Table
		{
			get { return table; }
		}

		public Field Field(string name)
		{
			return new Field(this, name);
		}

		public Field All()
		{
			return Queries.Field.All(this);
		}
	}
}
