using System.Collections.Generic;

namespace DT.Api.Authorizations
{
    public class UserTokenModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimModel> Claims { get; set; }
    }
}
