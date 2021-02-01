using DT.Api.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DT.Api.Authorizations
{
    public interface IAuthUser
    {
      
        Task<UserLoginResponse> GenerateJwt(string email);
        Task CreateDefaultUserAdminMaster(string email, string password);
        Task CreateDefaultRoles();

    }
}
