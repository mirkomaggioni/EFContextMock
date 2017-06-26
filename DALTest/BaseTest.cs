using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DAL.BusinessLayer;
using DAL.DataLayer;
using DAL.Test.Utils;
using NSubstitute;
using NUnit.Framework;

namespace DAL.Test
{
    public class BaseTest
    {
        protected PersonService PersonService;
        protected Context DbContext;

        [OneTimeSetUp]
        public void Setup()
        {
            var persons = new List<Person>() {
                new Person() { TaxCode = "taxcode1", Firstname = "firstname1", Surname = "surname1" },
                new Person() { TaxCode = "taxcode2", Firstname = "firstname2", Surname = "surname2" }
            }.AsQueryable();

            var mockSet = Substitute.For<DbSet<Person>, IQueryable<Person>, IDbAsyncEnumerable<Person>>();

            ((IQueryable<Person>)mockSet).Expression.Returns(persons.Expression);
            ((IQueryable<Person>)mockSet).ElementType.Returns(persons.ElementType);
            ((IQueryable<Person>)mockSet).GetEnumerator().Returns(persons.GetEnumerator());

            ((IQueryable<Person>)mockSet).Provider.Returns(new AsyncQueryProvider<Person>(persons.Provider));
            ((IDbAsyncEnumerable<Person>)mockSet).GetAsyncEnumerator().Returns(new AsyncEnumerator<Person>(persons.GetEnumerator()));

            DbContext = Substitute.For<Context>();
            DbContext.Persons.Returns(mockSet);

			var contextFactory = new ContextFactory();
			contextFactory.Register(DbContext);
            PersonService = new PersonService(contextFactory);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            
        }
    }
}
