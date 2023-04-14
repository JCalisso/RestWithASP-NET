using RestWithASPNet.Models;

namespace RestWithASPNet.Repository
{
    public interface IBookRepository
    {
        //Book Create(Book book);

        //Book FindById(long id);

        List<Book> FindAllBooks();

        //Book Update(Book book);

        //void Delete(long id);

        //bool Exists(long id);
    }
}
