using RestWithASPNet.Data.VO;

namespace RestWithASPNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(int id);

        List<PersonVO> FindAll();

        PersonVO Update(PersonVO person);

        PersonVO Disable(int id);

        void Delete(int id);
    }
}
