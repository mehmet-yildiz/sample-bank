using System.Linq;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Helpers;

namespace SampleBank.Business
{
    public class BusinessUser : BusinessBase<User>, IBusinessUser
    {
        public BusinessUser(IPersistenceBase<User> persistence, IUnitOfWork uow) : base(persistence, uow)
        {
        }

        public User AuthenticateUser(string username, string password)
        {
            username = username?.Trim();
            password = password?.Trim();

            var user = Persistence.GetAll().FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return null;
            }

            return !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }
    }
}