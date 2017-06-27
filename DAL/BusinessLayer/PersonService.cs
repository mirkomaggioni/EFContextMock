using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.DataLayer;

namespace DAL.BusinessLayer
{
    public class PersonService
    {
        private readonly ContextFactory _contextFactory;

        public PersonService(ContextFactory contextFactory)
        {
	        _contextFactory = contextFactory;
        }

        public async Task<List<Person>> GetPersons(string query)
        {
	        using (var db = _contextFactory.Get<Context>())
	        {
		        return await db.Persons.Where(p => p.TaxCode.Contains(query) || p.Firstname.Contains(query) || p.Surname.Contains(query)).ToListAsync();
			}
		}

        public async Task AddPerson(Person person)
        {
	        using (var db = _contextFactory.Get<Context>())
	        {
				db.Persons.Add(person);
		        await db.SaveChangesAsync();
	        }
        }

        public async Task UpdatePerson(Person person)
        {
	        using (var db = _contextFactory.Get<Context>())
	        {
		        var entity = await db.Persons.FirstOrDefaultAsync(p => p.TaxCode == person.TaxCode);
		        if (entity != null)
		        {
			        entity.Firstname = person.Firstname;
			        entity.Surname = person.Surname;
			        await db.SaveChangesAsync();
		        }
			}
        }

        public async Task DeletePerson(Person person)
        {
	        using (var db = _contextFactory.Get<Context>())
	        {
		        db.Persons.Remove(person);
		        await db.SaveChangesAsync();
	        }
        }
    }
}
