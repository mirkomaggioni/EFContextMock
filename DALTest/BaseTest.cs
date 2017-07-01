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

	    [SetUp]
        public void Setup()
        {
            var persons = new List<Person>() {
                new Person() { TaxCode = "taxcode1", Firstname = "firstname1", Surname = "surname1" },
                new Person() { TaxCode = "taxcode2", Firstname = "firstname2", Surname = "surname2" }
            };
	        var queryable = persons.AsQueryable();

            MockSet = new Mock<DbSet<Person>>();

	        MockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(queryable.Expression);
	        MockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	        MockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

	        MockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Person>(queryable.Provider));
	        MockSet.As<IDbAsyncEnumerable<Person>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Person>(queryable.GetEnumerator()));

	        MockSet.Setup(m => m.Add(It.IsAny<Person>())).Callback((Person person) => persons.Add(person));
	        MockSet.Setup(m => m.Remove(It.IsAny<Person>())).Callback((Person person) => persons.Remove(person));

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
