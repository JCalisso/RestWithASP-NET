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

        public List<Book> FindAll()
        {
            return _bookRepository.FindAll();
        }

        public Book FindById(int Id)
        {
            return _bookRepository.FindById(Id);
        }

        public Book Update(Book book)
        {
            return _bookRepository.Update(book);
        }

        public Book Create(Book book)
        {
            return _bookRepository.Create(book);
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}
