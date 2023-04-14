using RestWithASPNet.Repository;
using RestWithASPNet.Models;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Business;

namespace RestWithASPNet.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IBookRepository _bookRepository;
        public BookBusinessImplementation(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> FindAllBooks()
        {
            return _bookRepository.FindAllBooks();
        }
    }
}
