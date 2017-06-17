using System.Linq;
using System.Threading.Tasks;
using DAL.DataLayer;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DAL.Test
{
    [TestFixture]
    public class PersonServiceTest : BaseTest
    {
        [Test]
        public async Task Find_at_least_one_person()
        {
            var persons = await PersonService.GetPersons("taxcode1");

            persons.Should().NotBeNullOrEmpty();
            persons.Count().ShouldBeEquivalentTo(1);
        }

        [Test]
        public async Task New_person_is_saved()
        {
            var person = new Person()
            {
                TaxCode = "taxcode3",
                Firstname = "firstname3",
                Surname = "surname3"
            };

            await PersonService.AddPerson(person);

            DbContext.Persons.Received().Add(person);
            await DbContext.Received().SaveChangesAsync();
        }

        [Test]
        public async Task Modified_person_is_saved()
        {
            var person = new Person()
            {
                TaxCode = "taxcode1",
                Firstname = "firstname1",
                Surname = "surname1 changed"
            };

            DbContext.When(c => c.Attach(person)).DoNotCallBase();
            DbContext.When(c => c.SetModified(person)).DoNotCallBase();

            await PersonService.UpdatePerson(person);

            DbContext.Received().Attach(person);
            DbContext.Received().SetModified(person);
            await DbContext.Received().SaveChangesAsync();
        }

        [Test]
        public async Task Person_is_deleted()
        {
            var person = new Person()
            {
                TaxCode = "taxcode1",
                Firstname = "firstname1",
                Surname = "surname1"
            };

            DbContext.When(c => c.Attach(person)).DoNotCallBase();

            await PersonService.DeletePerson(person);

            DbContext.Received().Attach(person);
            DbContext.Persons.Received().Remove(person);
            await DbContext.Received().SaveChangesAsync();
        }
    }
}