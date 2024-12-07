using FluentValidation;
using WEB.Models.ViewModels.Account;

namespace WEB.FluentValidation.AccountValidators
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .EmailAddress()
                .WithMessage("Lütfen e-mail formatında giriş yapınız. Example: abc@example.com");
        }
    }
}
