using SampleBank.Core.Entity;
using SampleBank.Core.Model;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessCustomer : IBusinessBase<Customer>
    {
        ServiceResponse<Customer> GetCustomerInfo(int customerId);
    }
}