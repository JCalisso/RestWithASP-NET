using RestWithASPNet.Model;

namespace RestWithASPNet.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count = 0;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            return; 
        }

        // retorna uma listagem de pessoa
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i<8; i++)
            {
                Person person = MockPerson(1);
                persons.Add(person); 
            }

            return persons;
        }


        public Person FindById(long id)
        {
            return new Person
            {
                id = IncrementAndGet(),
                FirstName = "Jean",
                LastName = "Calisso",
                Address = "Fausto Fernandes Dias",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }


        private Person MockPerson(int i)
        {
            return new Person
            {
                id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
