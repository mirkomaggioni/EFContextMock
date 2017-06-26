using System;
using System.Data.Entity;

namespace DAL
{
	public class ContextFactory
	{
		private DbContext _dbContext;

		public void Register<TDbContext>(TDbContext dbContext) where TDbContext : DbContext, new()
		{
			_dbContext = dbContext;
		}

		public TDbContext Get<TDbContext>() where TDbContext : DbContext, new()
		{
			if (_dbContext == null)
			{
				return new TDbContext();
			}

			return (TDbContext)_dbContext;
		}
	}
}
