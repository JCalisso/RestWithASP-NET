using RestWithASPNet.Models;
using RestWithASPNet.Repository;
using RestWithASPNet.Data.Converter.Implementations;
using RestWithASPNet.Data.VO;

namespace RestWithASPNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        // Constructor
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        #region FindAll
        // retorna uma listagem de pessoa
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        #endregion

        #region FindById 
        public PersonVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        #endregion

        #region Create
        // Method responsible for creating a new person
        // If we had a database this would be the time to persist the data
        public PersonVO Create(PersonVO person)
        {
            //Quando um objeto chega, ele é um VO, por isso é necessário 
            //parcear ele antes de  persistir na base de dados como entidade, 
            //retornando o resultado convertendo novamente para Value Object (VO)
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);

            return _converter.Parse(personEntity);
        }
        #endregion

        #region Update
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);

            return _converter.Parse(personEntity);
        }
        #endregion

        #region Disable
        // Method responsible for disable a person from an ID
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
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
