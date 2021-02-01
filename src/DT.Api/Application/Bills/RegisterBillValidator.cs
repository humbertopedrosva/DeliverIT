using FluentValidation;

namespace DT.Api.Application.Bills
{
    public class RegisterBillValidator : AbstractValidator<RegisterBillCommand>
    {
        public RegisterBillValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Informe o nome");

            RuleFor(x => x.OriginalValue)
               .NotEmpty()
               .WithMessage("Informe o valor original");

            RuleFor(x => x.DueDate)
              .NotEmpty()
              .WithMessage("Informe a data de vencimento");

            RuleFor(x => x.PayDay)
             .NotEmpty()
             .WithMessage("Informe a data de pagamento");
        }
    }
}
