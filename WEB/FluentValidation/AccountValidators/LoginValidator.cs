using System.Text.RegularExpressions;
using FluentValidation;
using WEB.Models.ViewModels.Account;

namespace WEB.FluentValidation.AccountValidators;

public class LoginValidator : AbstractValidator<LoginVM>
{
  public LoginValidator()
  {
    var regex = new Regex("^[a-zA-Z.]+$");

    RuleFor(x => x.UserName)
      .NotEmpty()
      .WithMessage("Bu alan boş geçilemez!")
      .MaximumLength(250)
      .WithMessage("250 karakter sınırını geçtiniz!")
      .Matches(regex)
      .WithMessage("Sadece harf ve \".\" girebilirsiniz! Türkçe karakter kullanamazsınız!");
    
    RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage("Bu alan boş geçilemez!");

  }
}