using System;

namespace Enexure.Sql.Dynamic
{
	public sealed class Table : Expression
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

		public SelectExpression All()
		{
			return new SelectExpression(Dynamic.Field.All(new TableSource(this)));
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
