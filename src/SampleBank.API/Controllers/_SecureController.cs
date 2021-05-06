using Microsoft.AspNetCore.Authorization;

namespace SampleBank.API.Controllers
{
    [Authorize]
    public class SecureController : BaseController
    {
    }
}
