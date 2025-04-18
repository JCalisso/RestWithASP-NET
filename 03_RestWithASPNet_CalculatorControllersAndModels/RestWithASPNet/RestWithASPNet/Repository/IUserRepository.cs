using RestWithASPNet.Data.VO;
using RestWithASPNet.Models;

namespace RestWithASPNet.Repository
{
    public interface IUserRepository
    {
        User? ValidadeCredentials(UserVO user);
        User? ValidadeCredentials(string userName);

        bool RevokeToken(string userName);

        User RefreshUserInfo(User user);
    }
}
