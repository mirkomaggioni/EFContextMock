using System.Data.Entity;
using DAL.Interceptors;

namespace DAL
{
	public class DatabaseConfiguration : DbConfiguration
	{
		public DatabaseConfiguration()
		{
			SetDatabaseLogFormatter((context, writeAction) => new LogFormatter(context, writeAction));
			AddInterceptor(new CommandInterceptor());
		}
	}
}
