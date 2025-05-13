using RestWithASPNet.Models;
using RestWithASPNet.Models.Base;

namespace RestWithASPNet.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);

        T FindById(int id);

        List<T> FindAll();

        T Update(T item);

        void Delete(int id);

        bool Exists(int id);

        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);
    }
}
