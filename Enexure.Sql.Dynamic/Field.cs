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

		public Select As(string alias)
		{
			return new Select(this, alias);
		}

		public Select AsSelf()
		{
			return new Select(this, string.Empty);
		}

		public static Field All(DataSource dataSource)
		{
			return new Field(dataSource, "*");
		}

		public static Field All()
		{
			return new Field(null, "*");
		}
	}
}
