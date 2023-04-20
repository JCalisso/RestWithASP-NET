using RestWithASPNet.Models;

namespace RestWithASPNet.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);

        Person FindById(int id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(int id);

        bool Exists(int id);
    }
}
