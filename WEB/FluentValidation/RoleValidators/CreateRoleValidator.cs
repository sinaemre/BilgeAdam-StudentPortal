using FluentValidation;
using WEB.Areas.Admin.Models.ViewModels.Roles;

namespace WEB.FluentValidation.RoleValidators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleVM>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Bu alan boş geçilemez!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .MaximumLength(50)
                .WithMessage("50 karakter sınırını geçtiniz!");
        }
    }
}
