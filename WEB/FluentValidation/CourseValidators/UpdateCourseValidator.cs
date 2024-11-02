using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Education.Models.ViewModels.Courses;

namespace WEB.FluentValidation.CourseValidators
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseVM>
    {
        public UpdateCourseValidator()
        {
            var regex = new Regex("^[a-zA-Z0-9 ıİğĞöÖüÜçÇşŞ-]+$");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .MaximumLength(30)
                .WithMessage("En fazla 30 karakter girebilirsiniz!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk, sayı ve ' - ' girebilirsiniz!");
        }
    }
}
