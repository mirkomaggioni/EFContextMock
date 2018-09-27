using System;
using System.Data.Entity;
using DAL.DataLayer;
using DAL.Interfaces;

namespace DAL
{
	public class ContextFactory
	{
		private Type _dbContextType;
		private DbContext _dbContext;

		public void Register<TDbContext>(TDbContext dbContext) where TDbContext : DbContext, new()
		{
			_dbContextType = typeof(TDbContext);
			_dbContext = dbContext;
		}

		public TDbContext Get<TDbContext>(ILoggerService loggerService = null) where TDbContext : DbContext, IContext, new()
		{
			if (_dbContext == null || _dbContextType != typeof(TDbContext) || loggerService != null)
			{
				var db = new TDbContext();
				if (loggerService != null)
					db.Database.Log = (string message) => loggerService.Debug(message);

				return db;
			}

			return (TDbContext)_dbContext;
		}
	}
}
