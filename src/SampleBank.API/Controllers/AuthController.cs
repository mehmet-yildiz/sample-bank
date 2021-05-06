using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.API.Model.Auth;
using SampleBank.API.Model.JWT;
using SampleBank.Core.Abstractions.Business;

namespace SampleBank.API.Controllers
{
    public class AuthController : BaseController
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
            var response = new ApiResponse<AccessToken>();
            var serviceResponse = _authService.AuthenticateUser(loginModel.Username, loginModel.Password);
            if (serviceResponse.HasError || serviceResponse.Data == null)
            {
                response.ErrorMessage = "Username or password is not valid!";
                return Ok(response);
            }
            //var claims = _authService.GetClaims(user);
            var result = _tokenHelper.CreateToken(serviceResponse.Data, null);
            if (result != null)
            {
                response.Data = result;
                return Ok(response);
            }
            response.ErrorMessage = "Unexpected error!";
            return Ok(response);
        }
    }
}