using SampleBank.Core.Entity;
using SampleBank.Core.Model;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessUser : IBusinessBase<User>
    {
        ServiceResponse<User> AuthenticateUser(string username, string password);

    }
}