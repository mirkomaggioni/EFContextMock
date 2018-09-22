using System.Data.Entity;

namespace DAL
{
	public class DatabaseConfiguration : DbConfiguration
	{
		public DatabaseConfiguration()
		{
			SetDatabaseLogFormatter((context, writeAction) => new LogFormatter(context, writeAction));
		}
	}
}
