using System;

namespace SampleBank.API.Model.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } 
        public DateTime Expiration { get; set; } 
    }
}
