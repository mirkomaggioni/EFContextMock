using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using DAL.BusinessLayer;
using DAL.DataLayer;

namespace DAL.Interceptors
{
	public class CommandInterceptor : IDbCommandInterceptor
	{
		private static LoggerService _loggerService;

		public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			if (ToBeLogged(interceptionContext))
				_loggerService.Debug(command.CommandText);
		}

		private bool ToBeLogged<TContext>(TContext interceptionContext) where TContext : DbCommandInterceptionContext
		{
			var context = interceptionContext.DbContexts.FirstOrDefault();
			if (context != null && context is IContext && ((IContext)context).LogEnabled && !string.IsNullOrEmpty(((IContext)context).LogPath))
			{
				if (_loggerService == null)
					_loggerService = new LoggerService(((IContext)context).LogPath);

				return true;
			}

			return false;
		}
	}
}
