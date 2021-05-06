using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;

namespace SampleBank.API.Controllers
{
    public class TransactionController : SecureController
    {
        private readonly IBusinessTransaction _transactionService;

        public TransactionController(IBusinessTransaction transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet("list")]
        public ApiResponse<IEnumerable<Transaction>> GetTransactions()
        {
            var apiResponse = new ApiResponse<IEnumerable<Transaction>>();
            var serviceResponse = _transactionService.GetAll();

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