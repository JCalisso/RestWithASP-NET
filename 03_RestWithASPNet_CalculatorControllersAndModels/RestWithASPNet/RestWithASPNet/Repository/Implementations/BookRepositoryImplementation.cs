using RestWithASPNet.Models;
using RestWithASPNet.Models.Context;

namespace RestWithASPNet.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private SQLContext _sqlContext;

        public BookRepositoryImplementation (SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public List<Book> FindAll()
        {
            return _sqlContext.Books.ToList();
        }

        public Book FindById(int Id)
        {
            return _sqlContext.Books.SingleOrDefault(param => param.Id.Equals(Id));
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;  

            var result = _sqlContext.Books.SingleOrDefault(param => param.Id == book.Id);

            if (result != null)
            {
                try
                {
                    _sqlContext.Entry(result).CurrentValues.SetValues(book);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return book;
        }


        public Book Create(Book book)
        {
            try
            {
                _sqlContext.Add(book);
                _sqlContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return book;
        }


        public void Delete(int id)
        {

            try
            {
                var result = _sqlContext.Books.SingleOrDefault(param => param.Id.Equals(id));

                if (result != null)
                {
                    _sqlContext.Remove(result);
                    _sqlContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Exists (int id)
        {
            return _sqlContext.Books.Any(param => param.Id.Equals(id));
        }
    }
}
