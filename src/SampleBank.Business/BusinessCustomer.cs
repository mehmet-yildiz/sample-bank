using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Model;
using System.Linq;


namespace SampleBank.Business
{
    public class BusinessCustomer : BusinessBase<Customer>, IBusinessCustomer
    {
        public BusinessCustomer(IRepositoryBase<Customer> persistence, IUnitOfWork uow, ILogger logger) : base(persistence, uow, logger)
        {
        }

        public ServiceResponse<Customer> GetCustomerInfo(int customerId)
        {
            var response = new ServiceResponse<Customer>();

            var customer = Repository.GetAll(x => x.Accounts.Where(y => y.Transactions != null)).FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                response.ErrorMessage = "CustomerIsNull";
            }
            response.Data = customer;
            return response;
        }
    }
}