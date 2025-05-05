using RestWithASPNet.Models;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Repository.Generic;

namespace RestWithASPNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(SQLContext context) : base(context) { }

        public Person Disable(int id)
        {
            if (!_sqlContext.Persons.Any(p => p.Id.Equals(id))) return null;
            var person = _sqlContext.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (person != null)
            {
                person.Enabled = false;
                try
                {
                    _sqlContext.Entry(person).CurrentValues.SetValues(person);
                    _sqlContext.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
            return person;
        }

        public List<Person> FindByName(string ?firstName, string ?lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _sqlContext.Persons.Where(
                    p => p.FirstName.Contains(firstName) &&
                    p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return _sqlContext.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _sqlContext.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            return null;
        }
    }
}
