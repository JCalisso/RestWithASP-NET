using RestWithASPNet.Models;

namespace RestWithASPNet.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindById(int id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(int id);
    }
}
