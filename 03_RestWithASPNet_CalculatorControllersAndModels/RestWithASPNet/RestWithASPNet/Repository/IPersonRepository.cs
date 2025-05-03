using RestWithASPNet.Data.VO;
using RestWithASPNet.Models;

namespace RestWithASPNet.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(int id);
    }
}
