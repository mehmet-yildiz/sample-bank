using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.API.Model.JWT;
using SampleBank.Core.Abstractions.Business;

namespace SampleBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBusinessUser _authService;
        private readonly ITokenHelper _tokenHelper;

        public AuthController(IBusinessUser authService, ITokenHelper tokenHelper)
        {
            _authService = authService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginModel loginModel)
        {
            var user = _authService.AuthenticateUser(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return BadRequest();
            }
            //var claims = _authService.GetClaims(user);
            var result = _tokenHelper.CreateToken(user, null);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest((AccessToken)null);
        }
    }
}
