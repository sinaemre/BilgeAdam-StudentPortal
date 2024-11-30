using FluentValidation;
using WEB.Models.ViewModels.Account;

namespace WEB.FluentValidation.AccountValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!");

            RuleFor(x => x.CheckNewPassword)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .Matches(x => x.NewPassword)
                .WithMessage("Şifreler eşleşmiyor!");
        }
    }
}
