using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Model;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;

namespace SampleBank.API.Controllers
{
    public class CustomerController : SecureController
    {
        private readonly IBusinessCustomer _customerService;

        public CustomerController(IBusinessCustomer customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("list")]
        public ApiResponse<IEnumerable<Customer>> GetCustomers()
        {
            var apiResponse = new ApiResponse<IEnumerable<Customer>>();
            var serviceResponse = _customerService.GetAll();

            if (serviceResponse.HasError)
            {
                apiResponse.ErrorMessage = serviceResponse.ErrorMessage;
                return apiResponse;
            }

            apiResponse.Data = serviceResponse.Data.OrderByDescending(x => x.Id);
            return apiResponse;
        }

        [HttpGet("info")]
        public ApiResponse<Customer> GetCustomerInfo(int id)
        {
            var apiResponse = new ApiResponse<Customer>();
            var serviceResponse = _customerService.GetCustomerInfo(id);

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