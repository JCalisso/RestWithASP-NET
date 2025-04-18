using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Models.Base;
using RestWithASPNet.Models.Context;

namespace RestWithASPNet.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity

    {
        private SQLContext _sqlContext;

        private DbSet<T> dataset;

        // Constructor
        public GenericRepository(SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
            dataset = _sqlContext.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(int id)
        {
            return dataset.SingleOrDefault(param => param.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _sqlContext.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw; 
            }
        }

        public T Update(T item)
        { 
            var result = dataset.SingleOrDefault(param => param.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _sqlContext.Entry(result).CurrentValues.SetValues(item);
                    _sqlContext.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }


        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(param => param.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(int id)
        {
            return dataset.Any(param => param.Id.Equals(id));
        }

    }
}
