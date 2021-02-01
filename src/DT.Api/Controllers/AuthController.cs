using DT.Api.Authorizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DT.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthUser _authUser;

        public AuthController(SignInManager<IdentityUser> signInManager,
                             IAuthUser authUser)
        {
            _signInManager = signInManager;
            _authUser = authUser;
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            var validator = new UserModelValidator();

            var validatorResult = validator.Validate(userModel);

            if (!validatorResult.IsValid)
                return CustomResponseErrorValidation(validatorResult.Errors);

            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password,
                false, true);

            if (result.Succeeded)
                return CustomResponse(await _authUser.GenerateJwt(userModel.Email));


            if (result.IsLockedOut)
            {
                AddError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddError("Usuário ou Senha incorretos");
            return CustomResponse();
        }
    }
}
