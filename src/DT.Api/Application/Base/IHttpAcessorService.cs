using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DT.Api.Application.Base
{
    public interface IHttpAcessorService
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool Authenticated();
        IEnumerable<Claim> GetClaims();
        HttpContext ObterHttpContext();
    }
}
