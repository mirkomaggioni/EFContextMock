using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataLayer;

namespace DAL.BusinessLayer
{
    public class PersonService
    {
        private readonly IContext _context;

        public PersonService(IContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetPersons(string query)
        {
            return await _context.Persons.Where(p => p.TaxCode.Contains(query) || p.Firstname.Contains(query) || p.Surname.Contains(query)).ToListAsync();
        }

        public async Task AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            _context.Attach(person);
            _context.SetModified(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(Person person)
        {
            _context.Attach(person);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
