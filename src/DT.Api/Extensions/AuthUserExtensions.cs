using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DT.Api.Extensions
{
    public static class AuthUserExtensions
    {
        public static async Task<bool> UserExists<TUser>(this UserManager<TUser> userManager, string userName)
          where TUser : IdentityUser
        {
            return await userManager.FindByNameAsync(userName) != null;
        }

        public static async Task<bool> UserExistsById<TUser>(this UserManager<TUser> userManager, string id)
         where TUser : IdentityUser
        {
            return await userManager.FindByIdAsync(id) != null;
        }
    }
}
