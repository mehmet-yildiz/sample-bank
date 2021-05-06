using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Model.Account;

namespace SampleBank.API.Controllers
{
    public class AccountController : SecureController
    {
        private readonly IBusinessAccount _accountService;

        public AccountController(IBusinessAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public ApiResponse<bool> OpenAccount(OpenAccountModel model)
        {
            var apiResponse = new ApiResponse<bool>();
            var serviceResponse = _accountService.OpenAccount(model);

            if (serviceResponse.HasError)
            {
                apiResponse.ErrorMessage = serviceResponse.ErrorMessage;
                return apiResponse;
            }

            apiResponse.Data = serviceResponse.Data;
            return apiResponse;
        }
    }
}
