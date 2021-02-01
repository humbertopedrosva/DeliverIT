using DT.Api.Authorizations;

namespace DT.Api.Responses
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenModel UserToken { get; set; }
    }
}
