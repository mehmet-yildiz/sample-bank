using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.API.Model.Auth;
using SampleBank.API.Model.JWT;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;
using SampleBank.Core.Enums;
using SampleBank.Core.Helpers;
using System;

namespace SampleBank.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IBusinessUser _authService;
        private readonly IBusinessCustomer _customerService;
        private readonly IBusinessAccount _accountService;
        private readonly IBusinessTransaction _transactionService;
        private readonly ITokenHelper _tokenHelper;

        public AuthController(IBusinessUser authService, ITokenHelper tokenHelper, IBusinessCustomer customerService, IBusinessAccount accountService, IBusinessTransaction transactionService)
        {
            _authService = authService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
            _accountService = accountService;
            _transactionService = transactionService;
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

        /// <summary>
        /// Generates data for EF
        /// </summary>
        /// <returns></returns>
        [HttpPost("generateData")]
        public ActionResult GenerateData()
        {
            var response = new ApiResponse<bool>();

            HashingHelper.CreatePasswordHash("123", out var passwordHash, out var passwordSalt);

            var user1 = new User
            {
                Id = 1,
                Name = "mehmet",
                Surname = "yildiz",
                Username = "mehmet",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var customer1 = new Customer
            {
                Id = 1,
                Name = "John",
                Surname = "Doe"
            };
            var customer2 = new Customer
            {
                Id = 2,
                Name = "Rita",
                Surname = "Curie"
            };

            var account1 = new Account
            {
                Id = 1,
                Balance = 500,
                InitialCredit = 0,
                CustomerId = customer1.Id
            };
            var account2 = new Account
            {
                Id = 2,
                Balance = 1500,
                InitialCredit = 100,
                CustomerId = customer2.Id
            };
            var account3 = new Account
            {
                Id = 3,
                Balance = 4500,
                InitialCredit = 0,
                CustomerId = customer2.Id
            };

            var transaction1 = new Transaction { Id = 1, TransactionProcess = ProcessesType.Check, ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction2 = new Transaction { Id = 2, TransactionProcess = ProcessesType.Payment, ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction3 = new Transaction { Id = 3, TransactionProcess = ProcessesType.Transfer, ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction4 = new Transaction { Id = 4, TransactionProcess = ProcessesType.Swift, ProcessDate = DateTime.Now, AccountId = 2 };

            _authService.Insert(user1);

            _customerService.Insert(customer1);
            _customerService.Insert(customer2);

            _accountService.Insert(account1);
            _accountService.Insert(account2);
            _accountService.Insert(account3);

            _transactionService.Insert(transaction1);
            _transactionService.Insert(transaction2);
            _transactionService.Insert(transaction3);
            _transactionService.Insert(transaction4);

            response.Data = true;
            return Ok(response);
        }

        /// <summary>
        /// Checks generated data
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public ActionResult CheckGenerateData()
        {
            var response = new ApiResponse<bool>();
            var serviceResponse = _authService.GetById(1);

            response.Data = serviceResponse.Data != null;
            return Ok(response);
        }
    }
}