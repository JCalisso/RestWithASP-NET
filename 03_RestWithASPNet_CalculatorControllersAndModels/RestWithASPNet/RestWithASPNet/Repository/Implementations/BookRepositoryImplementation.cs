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

        public List<Book> FindAllBooks()
        {
            return _sqlContext.Books.ToList();
        }
    }
}
