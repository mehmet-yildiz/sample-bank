using SampleBank.Core.Entity;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessUser : IBusinessBase<User>
    {
        User AuthenticateUser(string username, string password);

    }
}