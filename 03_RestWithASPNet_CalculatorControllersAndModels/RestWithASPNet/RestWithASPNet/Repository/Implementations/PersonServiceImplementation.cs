using RestWithASPNet.Model;
using RestWithASPNet.Models.Context;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace RestWithASPNet.Repository.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private SQLContext _sqlContext;
        // Constructor
        public PersonServiceImplementation(SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        #region FindAll
        // retorna uma listagem de pessoa
        public List<Person> FindAll()
        {
            return _sqlContext.Persons.ToList();
        }
        #endregion

        #region FindById 
        public Person FindById(long id)
        {
            return _sqlContext.Persons.SingleOrDefault(param => param.Id.Equals(id));
        }
        #endregion

        #region Create
        // Method responsible for creating a new person
        // If we had a database this would be the time to persist the data
        public Person Create(Person person)
        {
            try
            {
                _sqlContext.Add(person);
                _sqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }
        #endregion

        #region Update
        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return new Person();

            var result = _sqlContext.Persons.SingleOrDefault(param => param.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _sqlContext.Entry(result).CurrentValues.SetValues(person);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return person;
        }
        #endregion

        #region Delete
        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            var result = _sqlContext.Persons.SingleOrDefault(param => param.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _sqlContext.Persons.Remove(result);
                    _sqlContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Exists
        private bool Exist(long id)
        {
            return _sqlContext.Persons.Any(param => param.Id.Equals(id));
        }
        #endregion
    }
}
