using RestWithASPNet.Models;
using RestWithASPNet.Repository;

namespace RestWithASPNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        // Constructor
        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
        }

        #region FindAll
        // retorna uma listagem de pessoa
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }
        #endregion

        #region FindById 
        public Person FindById(int id)
        {
            return _repository.FindById(id);
        }
        #endregion

        #region Create
        // Method responsible for creating a new person
        // If we had a database this would be the time to persist the data
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }
        #endregion

        #region Update
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
        #endregion

        #region Delete
        // Method responsible for deleting a person from an ID
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        #endregion
    }
}
