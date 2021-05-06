using System.Linq;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Helpers;
using SampleBank.Core.Model;

namespace SampleBank.Business
{
    public class BusinessUser : BusinessBase<User>, IBusinessUser
    {
        public BusinessUser(IRepositoryBase<User> persistence, IUnitOfWork uow, ILogger logger) : base(persistence, uow, logger)
        {
        }

        public ServiceResponse<User> AuthenticateUser(string username, string password)
        {
            var response = new ServiceResponse<User>();
            username = username?.Trim();
            password = password?.Trim();

            var user = Repository.GetAll().FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                response.ErrorMessage = "UserIsNull";
                return response;
            }

            response.Data = !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
            return response;
        }
    }
}