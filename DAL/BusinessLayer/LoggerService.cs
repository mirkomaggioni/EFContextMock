using System.IO;
using DAL.Interfaces;

namespace DAL.BusinessLayer
{
	public class LoggerService : ILoggerService
	{
		private readonly string _filePath;

		public LoggerService(string filepath)
		{
			var directorypath = filepath.Substring(0, filepath.LastIndexOf(@"\"));
			if (!Directory.Exists(directorypath))
				Directory.CreateDirectory(directorypath);

			_filePath = filepath;
		}

		public void Debug(string message)
		{
#if DEBUG
			using (var streamWriter = new StreamWriter(_filePath, true))
			{
				streamWriter.WriteLine(message);
			}
#endif
		}
	}
}
