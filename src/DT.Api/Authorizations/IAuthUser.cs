using DT.Api.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DT.Api.Authorizations
{
    public interface IAuthUser
    {
      
        Task<UserLoginResponse> GenerateJwt(string email);
        Task CreateDefaultUserAdminMaster(string email, string password);
        Task CreateDefaultRoles();
        Task<IdentityResult> CreateUser(UserModel userModeld);

    }
}
