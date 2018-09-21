using System.IO;
using DAL.Interfaces;

namespace DAL.BusinessLayer
{
	public class LoggerService : ILoggerService
	{
		private readonly string _filePath;

		public LoggerService(string filepath)
		{
			_filePath = filepath;
		}

		public void Debug(string message)
		{
			using (var streamWriter = new StreamWriter(_filePath, true))
			{
				streamWriter.WriteLine(message);
			}
		}
	}
}
