using RestWithASPNet.Data.VO;
using RestWithASPNet.Models;
using RestWithASPNet.Models.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLContext _context;

        public UserRepository(SQLContext context)
        {
            _context = context;
        }

        public User? ValidadeCredentials(UserVO user)
        {
            string pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User? ValidadeCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));

            if (user is null) return false;

            user.RefreshToken = ""; // Devido ao retorno do ValidadeCredentials, precisa passa "" ao invés de null

            _context.SaveChanges();

            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(param => param.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        // Retorna uma st ring encriptada
        private string ComputeHash(string input, HashAlgorithm algorith)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorith.ComputeHash(inputBytes);

            // após deprecated foi ajustado para a forma a baixo
            var builder = new StringBuilder();
            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        }

    }
}
