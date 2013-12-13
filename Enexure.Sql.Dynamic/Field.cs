using System;

namespace Enexure.Sql.Dynamic
{
	public class Field : Expression
	{
		private readonly DataSource dataSource;
		private readonly string name;

		public Field(DataSource dataSource, string name)
		{
			this.dataSource = dataSource;
			this.name = name;
		}

		public DataSource DataSource
		{
			get { return dataSource; }
		}

		public string Name
		{
			get { return name; }
		}

		public SelectExpression As(string alias)
		{
			return new SelectExpression(this, alias);
		}

		public static Field All(DataSource dataSource)
		{
			return new Field(dataSource, "*");
		}
	}
}
