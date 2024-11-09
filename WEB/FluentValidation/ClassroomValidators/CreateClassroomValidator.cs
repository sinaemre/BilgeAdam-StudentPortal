using FluentValidation;
using WEB.Areas.Education.Models.ViewModels.Classrooms;

namespace WEB.FluentValidation.ClassroomValidators
{
    public class CreateClassroomValidator : AbstractValidator<CreateClassroomVM>
    {
        public CreateClassroomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!")
                .MaximumLength(100)
                .WithMessage("100 karakter sınırını aştınız!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!");

            RuleFor(x => x.Description)
               .NotEmpty()
               .WithMessage("Bu alan zorunludur!")
               .MaximumLength(200)
               .WithMessage("100 karakter sınırını aştınız!")
               .MinimumLength(3)
               .WithMessage("En az 3 karakter girmelisiniz!");

            RuleFor(x => x.TeacherId)
                .NotEmpty()
                .WithMessage("Bu alan zorunludur!");

        }
    }
}
