using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNet.Business;
using RestWithASPNet.Data.VO;
using RestWithASPNet.Models;

namespace RestWithASPNet.Controllers
{
    [ApiVersion("1")]    
    [Route("api/[controller]/v{verion:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signing")]
        public IActionResult Signing([FromBody] UserVO user)
        {
            if (user is null) return BadRequest("Invalid client request.");

            var token = _loginBusiness.ValidateCredentials(user);

            if (token is null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh ([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Invalid client request.");

            var token = _loginBusiness.ValidateCredentials(tokenVo);

            if (token == null) return BadRequest("Invalid client request.");

            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")] // precisa estar autenticado pra saber qual token irá revogar
        [Route("revoke")]
        public IActionResult Revoke ()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);

            if (!result) return BadRequest("Invalid client request.");

            return NoContent();
        }
    }
}
