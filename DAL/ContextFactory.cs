using System;
using System.Data.Entity;
using DAL.BusinessLayer;
using DAL.Interfaces;

namespace DAL
{
	public class ContextFactory
	{
		private Type _dbContextType;
		private DbContext _dbContext;
		private ILoggerService _loggerService;

		public void Register<TDbContext>(TDbContext dbContext) where TDbContext : DbContext, new()
		{
			_dbContextType = typeof(TDbContext);
			_dbContext = dbContext;
		}

		public TDbContext Get<TDbContext>(bool log = false, string filepath = "") where TDbContext : DbContext, new()
		{
			if (log && string.IsNullOrEmpty(filepath))
				throw new ArgumentNullException(nameof(filepath));

			if (log && _loggerService == null)
				_loggerService = new LoggerService(filepath);

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
