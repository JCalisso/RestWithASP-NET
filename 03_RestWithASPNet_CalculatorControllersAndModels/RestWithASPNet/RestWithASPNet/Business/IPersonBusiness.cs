using RestWithASPNet.Data.VO;
using RestWithASPNet.Hypermedia.Utils;

namespace RestWithASPNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(int id);

        List<PersonVO> FindByName(string ?firstName, string ?lastName);

        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage);

        PersonVO Update(PersonVO person);

        PersonVO Disable(int id);

        void Delete(int id);
    }
}
