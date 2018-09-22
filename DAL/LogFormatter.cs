using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace DAL
{
	public class LogFormatter : DatabaseLogFormatter
	{
		public LogFormatter(Action<string> writeAction) : base(writeAction) {}
		public LogFormatter(DbContext context, Action<string> writeAction) : base(context, writeAction) {}

		public override void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Write($"Context '{Context.GetType().Name}' is executing command '{command.CommandText}'{Environment.NewLine}");
		}
	}
}
