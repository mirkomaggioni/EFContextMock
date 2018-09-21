using System.Data.Entity;
using System.Threading.Tasks;
using DAL.BusinessLayer;
using DAL.DataLayer;
using NUnit.Framework;

namespace DAL.Test.Integration
{
	[TestFixture]
	public class LoggerServiceTest
	{
		private ContextFactory _contextFactory;

		[SetUp]
		public void Setup()
		{
			_contextFactory = new ContextFactory(new LoggerService(@"c:\temp\log.txt"));
		}

		[Test]
		public async Task entity_framework_query_is_loggedAsync()
		{
			using (var db = _contextFactory.Get<Context>(true))
			{
				var person = await db.Persons.FirstOrDefaultAsync();
				Assert.IsNotNull(person);
			}
		}
	}
}
