using FluentValidation;
using WEB.Models.ViewModels.Account;

namespace WEB.FluentValidation.AccountValidators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!");

            RuleFor(x => x.CheckNewPassword)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .Matches(x => x.NewPassword)
                .WithMessage("Şifreler eşleşmemektedir!");

        }
    }
}
