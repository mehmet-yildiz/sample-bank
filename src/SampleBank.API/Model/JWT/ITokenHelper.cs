using System.Collections.Generic;
using SampleBank.Core.Entity;

namespace SampleBank.API.Model.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<SampleBank.Core.Entity.Claim> operationClaims);
    }
}
