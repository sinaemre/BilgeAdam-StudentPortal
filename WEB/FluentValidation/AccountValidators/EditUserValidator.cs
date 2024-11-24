using FluentValidation;
using WEB.Models.ViewModels.Account;

namespace WEB.FluentValidation.AccountValidators;

public class EditUserValidator : AbstractValidator<EditUserVM>
{
  public EditUserValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage("Bu alan zorunludur!")
      .EmailAddress()
      .WithMessage("Lütfen mail formatında giriş yapınız!");
  }
}