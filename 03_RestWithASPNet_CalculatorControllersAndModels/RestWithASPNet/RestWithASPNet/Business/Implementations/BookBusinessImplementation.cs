using RestWithASPNet.Repository;
using RestWithASPNet.Models;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Business;

namespace RestWithASPNet.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(int Id)
        {
            return _repository.FindById(Id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
