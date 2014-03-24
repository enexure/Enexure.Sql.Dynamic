using System;

namespace Enexure.Sql.Dynamic.Queries
{
	public sealed class Table
	{
		private readonly string name;

		public Table(string name)
		{
			this.name = name;
		}

		public string Name
		{
			get { return name; }
		}

		public Field All()
		{
			return Queries.Field.All(new TableSource(this));
		}

		public TableSource As(string alias)
		{
			return new TableSource(this, alias);
		}

		public Field Field(string name)
		{
			return new Field(new TableSource(this), name);
		}
	}
}
