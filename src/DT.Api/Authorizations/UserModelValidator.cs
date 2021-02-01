using FluentValidation;

namespace DT.Api.Authorizations
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O campo email é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("O campo senha é obrigatório");
        }
    }
}
