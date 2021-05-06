using SampleBank.Core.Entity;
using SampleBank.Core.Model;
using SampleBank.Core.Model.Account;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessAccount : IBusinessBase<Account>
    {
        ServiceResponse<bool> OpenAccount(OpenAccountModel model);
    }
}