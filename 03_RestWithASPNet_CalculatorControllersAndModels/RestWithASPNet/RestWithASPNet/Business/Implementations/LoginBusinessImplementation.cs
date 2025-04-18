using Microsoft.IdentityModel.Tokens;
using RestWithASPNet.Configurations;
using RestWithASPNet.Data.VO;
using RestWithASPNet.Repository;
using RestWithASPNet.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithASPNet.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {

        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;

        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            //Recupera o user na base 
            var user = _repository.ValidadeCredentials(userCredentials);
            //Se o user for null retorna null
            if (user == null) return null;
            //Se não for nulo, ele gera as demais informações

            //Gera as Claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            //Gera o accessToken
            var accessToken = _tokenService.GenerateAccessToken(claims);
            //Gera o refreshToken
            var refreshToken = _tokenService.GenerateRefreshToken();  //Quando o accessToken estiver expirado ele gera o refresh

            //Seta os valores no user que foi recuperado do banco
            user.RefreshToken = refreshToken;
            user.RefreshTokeneExpireTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            //Atualiza as informações do usuário no banco
            _repository.RefreshUserInfo(user);

            //Define quando foi gerado o Token
            DateTime createDate = DateTime.Now;
            //Define quando vai expirar o token (hora de criação mais os minutos definidos no appsettings)
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            //Seta as informações do Token
            return new TokenVO(
                true, //autenticado
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;
            var user = _repository.ValidadeCredentials(userName);

            if ((user == null) || 
                (user.RefreshToken != refreshToken) || 
                (user.RefreshTokeneExpireTime <= DateTime.Now))
                return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true, //autenticado
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
