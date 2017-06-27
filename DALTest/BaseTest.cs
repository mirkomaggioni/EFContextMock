using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DAL.BusinessLayer;
using DAL.DataLayer;
using DAL.Test.Utils;
using Moq;
using NUnit.Framework;

namespace DAL.Test
{
    public class BaseTest
    {
        protected PersonService PersonService;
	    protected Mock<Context> MockContext;
	    protected Mock<DbSet<Person>> MockSet;

	    [OneTimeSetUp]
        public void Setup()
        {
            var persons = new List<Person>() {
                new Person() { TaxCode = "taxcode1", Firstname = "firstname1", Surname = "surname1" },
                new Person() { TaxCode = "taxcode2", Firstname = "firstname2", Surname = "surname2" }
            }.AsQueryable();

            MockSet = new Mock<DbSet<Person>>();

	        MockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
	        MockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
	        MockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator);

	        MockSet.As<IQueryable<Person>>().Setup(m => m.Provider)
		        .Returns(new AsyncQueryProvider<Person>(persons.Provider));
	        MockSet.As<IDbAsyncEnumerable<Person>>().Setup(m => m.GetAsyncEnumerator())
		        .Returns(new AsyncEnumerator<Person>(persons.GetEnumerator()));

            MockContext = new Mock<Context>();
	        MockContext.Setup(m => m.Persons).Returns(MockSet.Object);

			var contextFactory = new ContextFactory();
			contextFactory.Register(MockContext.Object);
            PersonService = new PersonService(contextFactory);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            
        }
    }
}
