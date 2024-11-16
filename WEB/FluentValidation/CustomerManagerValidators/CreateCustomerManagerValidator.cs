using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Admin.Models.ViewModels.CustomerManagers;

namespace WEB.FluentValidation.CustomerManagerValidators
{
    public class CreateCustomerManagerValidator : AbstractValidator<CreateCustomerManagerVM>
    {
        public CreateCustomerManagerValidator()
        {
            var regex = new Regex("^[a-zA-Z ıİğĞöÖüÜçÇşŞ-]+$");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk ve ' - ' girebilirsiniz!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .MaximumLength(150)
                .WithMessage("En fazla 150 karakter girebilirsiniz!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk ve ' - ' girebilirsiniz!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!");

            RuleFor(x => x.HireDate)
               .NotEmpty()
               .WithMessage("Bu alan zorunludur!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .EmailAddress()
                .WithMessage("E-Mail formatında giriş yapınız!");
        }
    }
}
