using RestWithASPNet.Data.VO;

namespace RestWithASPNet.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindById(int id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        void Delete(int id);
    }
}
