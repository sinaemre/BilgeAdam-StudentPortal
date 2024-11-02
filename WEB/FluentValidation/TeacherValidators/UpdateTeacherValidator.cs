using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.FluentValidation.TeacherValidators
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherVM>
    {
        public UpdateTeacherValidator()
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

            RuleFor(x => x.CourseId)
               .NotEmpty()
               .WithMessage("Bu alan zorunludur!");
        }
    }
}
