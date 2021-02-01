using DT.Api.Extensions;
using DT.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DT.Api.Authorizations
{
    public class AuthUser : IAuthUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        public AuthUser(
                       UserManager<IdentityUser> userManager,
                       RoleManager<IdentityRole> roleManager,
                       IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }
        public async Task<UserLoginResponse> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetClaimsByUser(claims, user);
            var encodedToken = EncodeToken(identityClaims);

            return GetTokenResponse(encodedToken, user, claims);

        }

        public async Task<IdentityResult> CreateUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                EmailConfirmed = true
            };

           var result =  await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "User");

            return result;
        }
        public async Task CreateDefaultUserAdminMaster(string email, string password)
        {
            var userExists = await _userManager.UserExists<IdentityUser>(email);

            if (!userExists)
                await SaveUserAsync(email, password);
        }

        public async Task CreateDefaultRoles()
        {
            string[] rolesNames = { "AdminMaster", "User" };

            foreach (var namesRole in rolesNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(namesRole);

                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole(namesRole));
            }
        }

        private async Task<ClaimsIdentity> GetClaimsByUser(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));


            foreach (var userRole in userRoles)
                claims.Add(new Claim("role", userRole));


            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserLoginResponse GetTokenResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaimModel { Type = c.Type, Value = c.Value })
                }
            };
        }
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        private async Task<bool> SaveUserAsync(string userName, string password)
        {
            var defaultUser = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(defaultUser, password);

            if (result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(defaultUser, "AdminMaster");

                return resultRole.Succeeded;
            }

            return result.Succeeded;
        }

       
    }
}
