using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;
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

        /// <summary>
        /// Opens the account connected to user with provided CustomerId and decide sending a transaction by initialCredit parameter value. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("open")]
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

        [HttpGet("list")]
        public ApiResponse<IEnumerable<Account>> GetAccounts()
        {
            var apiResponse = new ApiResponse<IEnumerable<Account>>();
            var serviceResponse = _accountService.GetAll();

            if (serviceResponse.HasError)
            {
                apiResponse.ErrorMessage = serviceResponse.ErrorMessage;
                return apiResponse;
            }

            apiResponse.Data = serviceResponse.Data.OrderByDescending(x => x.Id);
            return apiResponse;
        }
    }
}
