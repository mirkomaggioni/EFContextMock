using System;
using System.Data.Entity;
using DAL.Interfaces;

namespace DAL
{
	public class ContextFactory
	{
		private Type _dbContextType;
		private DbContext _dbContext;
		private readonly ILoggerService _loggerService;

		public ContextFactory(ILoggerService loggerService)
		{
			_loggerService = loggerService;
		}

		public void Register<TDbContext>(TDbContext dbContext) where TDbContext : DbContext, new()
		{
			_dbContextType = typeof(TDbContext);
			_dbContext = dbContext;
		}

		public TDbContext Get<TDbContext>(bool log = false) where TDbContext : DbContext, new()
		{
			if (_dbContext == null || _dbContextType != typeof(TDbContext))
			{
				var db = new TDbContext();
				if (log)
					db.Database.Log = (string message) => _loggerService.Debug(message);

				return db;
			}

			return (TDbContext)_dbContext;
		}
	}
}
